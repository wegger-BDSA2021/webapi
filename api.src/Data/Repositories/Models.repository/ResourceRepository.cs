using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
// using Microsoft.EntityFrameworkCore;
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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Resource>> GetAllFromUserAsync(int userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Resource>> GetAllFromDomainAsync(string domain)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Resource>> GetAllWithTagsAsyc(ICollection<Tag> tags)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Resource>> GetAllWithRatingAsync(int rating)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Resource>> GetAllWhereTitleContainsAsync(string matcher)
        {
            throw new System.NotImplementedException();
        }
    }
}