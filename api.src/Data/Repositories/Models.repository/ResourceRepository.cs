using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using static Data.Response;
using Microsoft.EntityFrameworkCore;
using Utils;
using System;

namespace Data
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly IWeggerContext _context;

        private Func<Resource, int, int, bool> inRange = (resource, from, to) => resource.Ratings.Select(r => r.Rated).Average() > from;// .IsWithin(from, to); 

        public ResourceRepository(IWeggerContext context)
        {
            _context = context;
        }

        public async Task<(Response Response, ResourceDetailsDTO ResourceDetails)> ReadAsync(int id)
        {
            var _resourceEntity = await _context.Resources.FindAsync(id);
            if (_resourceEntity is null)
                return (NotFound, null);

            var result = new ResourceDetailsDTO(
                _resourceEntity.Id,
                _resourceEntity.Title,
                _resourceEntity.Description,
                _resourceEntity.TimeOfReference,
                _resourceEntity.TimeOfResourcePublication,
                _resourceEntity.Url,
                _resourceEntity.Tags.Select(t => t.Name).ToList(),
                _resourceEntity.Ratings.Select(r => r.Rated).ToList(),
                _resourceEntity.Ratings.Select(r => r.Rated).Average(),
                _resourceEntity.Comments.Select(c => c.Content).ToList(),
                _resourceEntity.Deprecated,
                _resourceEntity.LastCheckedForDeprecation
            );

            return (OK, result);
        }

        public async Task<IReadOnlyCollection<ResourceDTO>> ReadAllAsync()
           => (await _context.Resources
                .Select(r => 
                    new ResourceDTO(
                        r.Id,
                        r.Title,
                        r.Description,
                        r.Url,
                        r.Ratings.Select(r => r.Rated).Average(),
                        r.Deprecated
                    ))
                .ToListAsync())
            .AsReadOnly();
        
        public async Task<(Response Response, ResourceDetailsDTO CreatedResource)> CreateAsync(ResourceCreateDTOServer resource)
        {
            var linkExists = await _context.Resources.FirstOrDefaultAsync(r => r.Url == resource.Url);

            if (linkExists is not null)
                return (Conflict, null);

            var _resourceEntity = new Resource
            {
                Title = resource.Title,
                UserId = resource.UserId,
                Description = resource.Description,
                TimeOfReference = resource.TimeOfReference,
                TimeOfResourcePublication = resource.TimeOfResourcePublication,
                Url = resource.Url,
                Deprecated = resource.Deprecated,
                LastCheckedForDeprecation = resource.LastCheckedForDeprecation,
                Tags = await getTagsFromStringsAsync(resource.Tags)
            };

            await _context.Resources.AddAsync(_resourceEntity);
            await _context.SaveChangesAsync();

            var _initialRating = new Rating {
                UserId = _resourceEntity.UserId,
                ResourceId = _resourceEntity.Id,
                Rated =  resource.InitialRating
            };
            
            await _context.Ratings.AddAsync(_initialRating);
            await _context.SaveChangesAsync();

            ResourceDetailsDTO result = new ResourceDetailsDTO(
                _resourceEntity.Id,
                _resourceEntity.Title,
                _resourceEntity.Description,
                _resourceEntity.TimeOfReference,
                _resourceEntity.TimeOfResourcePublication,
                _resourceEntity.Url,
                _resourceEntity.Tags.Select(t => t.Name).ToList(),
                _resourceEntity.Ratings.Select(r => r.Rated).ToList(),
                _initialRating.Rated,
                _resourceEntity.Comments.Select(c => c.Content).ToList(),
                _resourceEntity.Deprecated,
                _resourceEntity.LastCheckedForDeprecation
            );

            return (Created, result);
        }

        public async Task<Response> DeleteAsync(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource is null)
                return NotFound;

            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();

            return Deleted;
        }

        public async Task<Response> UpdateAsync(ResourceUpdateDTO resource)
        {
            var entity = await _context.Resources.FirstOrDefaultAsync(r => r.Id == resource.Id);

            if (entity is null)
                return NotFound;

            entity.Title = resource.Title;
            entity.Description = resource.Description;
            entity.UserId = (int)resource.UserId;
            // entity.TimeOfReference = resource.TimeOfReference;
            entity.TimeOfResourcePublication = (System.DateTime)resource.TimeOfResourcePublication;
            entity.Url = resource.Url;
            entity.Tags = await getTagsFromStringsAsync(resource.Tags);
            entity.Deprecated = (bool)resource.Deprecated;
            entity.LastCheckedForDeprecation = (System.DateTime)resource.LastCheckedForDeprecation;

            await _context.SaveChangesAsync();

            return Updated;
        }

        public async Task<IReadOnlyCollection<ResourceDTO>> GetAllDeprecatedAsync()
        {
            return (await _context.Resources
                .Where(r => r.Deprecated == true)
                    .Select(
                        r => new ResourceDTO(
                            r.Id,
                            r.Title,
                            r.Description,
                            r.Url,
                            r.Ratings.Select(rt => rt.Rated).Average(),
                            r.Deprecated
                )).ToListAsync())
            .AsReadOnly();
        }

        public async Task<IReadOnlyCollection<ResourceDTO>> GetAllFromUserAsync(int userId)
        {
            return (await _context.Resources
                .Where(r => r.UserId == userId)
                    .Select(
                        r => new ResourceDTO(
                            r.Id,
                            r.Title,
                            r.Description,
                            r.Url,
                            r.Ratings.Select(rt => rt.Rated).Average(),
                            r.Deprecated
                )).ToListAsync())
            .AsReadOnly();
        }

        public async Task<IReadOnlyCollection<ResourceDTO>> GetAllFromDomainAsync(string domain)
        {
            var lower = domain.ToLower();
            return (await _context.Resources
                .Where(r => r.Url.ToLower().Contains(domain))
                    .Select(
                        r => new ResourceDTO(
                            r.Id,
                            r.Title,
                            r.Description,
                            r.Url,
                            r.Ratings.Select(rt => rt.Rated).Average(),
                            r.Deprecated
                )).ToListAsync())
            .AsReadOnly();
        }

        public async Task<IReadOnlyCollection<ResourceDTO>> GetAllWithTagsAsyc(ICollection<string> stringTags)
        {
            var tags = await getTagsFromStringsAsync(stringTags);
            return (await _context.Resources
                .Include(r => r.Tags)
                    .Where(r => r.Tags.Any(c => tags.Contains(c)))
                        .Select(
                            r => new ResourceDTO(
                                r.Id,
                                r.Title,
                                r.Description,
                                r.Url,
                                r.Ratings.Select(rt => rt.Rated).Average(),
                                r.Deprecated
                    )).ToListAsync())
                .AsReadOnly();
        }

        public async Task<IReadOnlyCollection<ResourceDTO>> GetAllWithRatingInRangeAsync(int from, int to)
        {

            // computing the query client side (not optimal, and cannot be made async) 
            // var toBeQueried = _context.Resources.AsEnumerable();
            // return (toBeQueried
            //     .Where(r => inRange(r, from, to))
            //     .Select(
            //         r => new ResourceDTO(
            //             r.Id,
            //             r.Title,
            //             r.Description,
            //             r.Url,
            //             r.Ratings.Select(rt => rt.Rated).Average(),
            //             r.Deprecated
            //         )))
            //     .ToList()
            // .AsReadOnly();



            // computing the query server side 
            // (better solution, but stupid double where clause not using my hot extension methods ... ) 
            return (await _context.Resources
                .Include(r => r.Ratings)
                    .Where(r => r.Ratings.Select(rt => rt.Rated).Average() >= from)
                    .Where(r => r.Ratings.Select(rt => rt.Rated).Average() <= to)
                        .Select(
                            r => new ResourceDTO(
                                r.Id,
                                r.Title,
                                r.Description,
                                r.Url,
                                r.Ratings.Select(rt => rt.Rated).Average(),
                                r.Deprecated
                            ))
                    .ToListAsync())
                .AsReadOnly();
        }

        public async Task<IReadOnlyCollection<ResourceDTO>> GetAllWhereTitleContainsAsync(string matcher)
        {
            return (await _context.Resources
                .Where(r => r.Title.Contains(matcher))
                    .Select(
                        r => new ResourceDTO(
                            r.Id,
                            r.Title,
                            r.Description,
                            r.Url,
                            r.Ratings.Select(rt => rt.Rated).Average(),
                            r.Deprecated
                )).ToListAsync())
            .AsReadOnly();
        }

        public async Task<(Response Response, double Average)> GetAverageRatingByIdAsync(int resourceId)
        {
            var exists = await _context.Resources.FindAsync(resourceId);
            if (exists is null)
                return (NotFound, -1);

            var result = exists.Ratings.Select(r => r.Rated).Average();
            return(OK, result);
        }


        // private helper methods
        private async Task<ICollection<Tag>> getTagsFromStringsAsync(IEnumerable<string> tags)
        {
            var lowerCaseLetters = tags.Select(t => t.ToLower());
            var tagsFromString = await _context.Tags.Where(t => tags.Contains(t.Name.ToLower())).ToListAsync();

            return tagsFromString;
        }

    }
}