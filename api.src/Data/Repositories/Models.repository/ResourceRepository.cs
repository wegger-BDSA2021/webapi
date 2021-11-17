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
    }
}