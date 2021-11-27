using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using static Data.Response;
using Microsoft.EntityFrameworkCore;
using Utils;

namespace Data
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly IWeggerContext _context;
        public ResourceRepository(IWeggerContext context)
        {
            _context = context;
        }

        public async Task<(Response Response, Resource Resource)> ReadAsync(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource is null)
                return (NotFound, null);

            return (OK, resource);
        }

        public async Task<IReadOnlyCollection<Resource>> ReadAllAsync()
            => (await _context.Resources.ToListAsync()).AsReadOnly();

        public async Task<(Response Response, ResourceDetailsDTO CreatedResource)> CreateAsync(ResourceCreateDTO resource)
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
            entity.TimeOfReference = resource.TimeOfReference;
            entity.TimeOfResourcePublication = resource.TimeOfResourcePublication;
            entity.Url = resource.Url;
            entity.Deprecated = resource.Deprecated;
            entity.LastCheckedForDeprecation = resource.LastCheckedForDeprecation;
            entity.UserId = resource.UserId;

            await _context.SaveChangesAsync();

            return Updated;
        }

        public async Task<IReadOnlyCollection<Resource>> GetAllDeprecatedAsync()
        {
            return (await _context.Resources.Where(r => r.Deprecated == true).ToListAsync()).AsReadOnly();
        }

        public async Task<IReadOnlyCollection<Resource>> GetAllFromUserAsync(int userId)
        {
            return (await _context.Resources.Where(r => r.UserId == userId).ToListAsync()).AsReadOnly();
        }

        public async Task<IReadOnlyCollection<Resource>> GetAllFromDomainAsync(string domain)
        {
            return (await _context.Resources.Where(r => r.Url.Contains(domain)).ToListAsync()).AsReadOnly();
        }

        public async Task<IReadOnlyCollection<Resource>> GetAllWithTagsAsyc(ICollection<string> stringTags)
        {
            var tags = await getTagsFromStringsAsync(stringTags);
            return (await _context.Resources.Include(r => r.Tags).Where(r => r.Tags.Any(c => tags.Contains(c))).ToListAsync()).AsReadOnly();
        }

        public async Task<IReadOnlyCollection<Resource>> GetAllWithRatingInRangeAsync(int from, int to)
        {
            return (await _context.Resources.Include(r => r.Ratings).Where(r => r.Ratings.Select(rt => rt.Rated).Average().IsWithin(from, to)).ToListAsync()).AsReadOnly();
        }

        public async Task<IReadOnlyCollection<Resource>> GetAllWhereTitleContainsAsync(string matcher)
        {
            return (await _context.Resources.Where(r => r.Title.Contains(matcher)).ToListAsync()).AsReadOnly();
        }

        public async Task<(Response, double)> GetAverageRatingByIdAsync(int resourceId)
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