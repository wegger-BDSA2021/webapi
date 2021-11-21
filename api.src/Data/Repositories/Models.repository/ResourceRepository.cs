using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
// using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly IWeggerContext _context;
        public ResourceRepository(IWeggerContext context)
        {
            _context = context;
        }

        public async Task<Resource> GetEntityByIdAsync(int id)
        {
            // return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            throw new System.Exception();
        }

        public async Task<List<Resource>> GetAllEntitiesAsync()
        {
            // return await GetAll().ToListAsync();
            throw new System.Exception();
        }

        public async Task<int> CreateResourceAsync(Resource resource)
        {
            throw new System.NotImplementedException();
        }

        public async Task<(Response Response, Resource Resource)> ReadAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Resource>> ReadAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<(Response Response, int ResourceId)> CreateAsync(Resource resource)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
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