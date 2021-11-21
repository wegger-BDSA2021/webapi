using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using static Data.Response;
using Microsoft.EntityFrameworkCore;

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

        public async Task<(Response Response, int ResourceId)> CreateAsync(Resource resource)
        {
            var linkExists = await _context.Resources.FirstOrDefaultAsync(r => r.Url == resource.Url);

            if (linkExists is not null)
                return (Conflict, -1);

            var _resource = new Resource
            {
                Title = resource.Title,
                User = resource.User,
                Description = resource.Description,
                TimeOfReference = resource.TimeOfReference,
                TimeOfResourcePublication = resource.TimeOfResourcePublication,
                Url = resource.Url,
                Deprecated = resource.Deprecated,
                LastCheckedForDeprecation = resource.LastCheckedForDeprecation,
                Tags = resource.Tags,
                Ratings = resource.Ratings, 
            };

            await _context.Resources.AddAsync(_resource);
            await _context.SaveChangesAsync();

            return (Created, _resource.Id);

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

        public async Task<Response> UpdateAsync(Resource resource)
        {
            throw new System.NotImplementedException();
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

        public async Task<IReadOnlyCollection<Resource>> GetAllWithTagsAsyc(ICollection<Tag> tags)
        {
            // var stringTags = tags.Select(t => t.Name);
            return (await _context.Resources.Include(r => r.Tags).Where(r => r.Tags.Any(c => tags.Contains(c))).ToListAsync()).AsReadOnly();
        }

        public async Task<IReadOnlyCollection<Resource>> GetAllWithRatingAsync(int rating)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Resource>> GetAllWhereTitleContainsAsync(string matcher)
        {
            return (await _context.Resources.Where(r => r.Title.Contains(matcher)).ToListAsync()).AsReadOnly();
        }
    }
}