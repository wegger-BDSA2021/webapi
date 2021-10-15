using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
// using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class EntityRepository : Repository<EntityT>, IEntityRepository
    {
        public EntityRepository(FooContext context) : base(context)
        {
        }

        public async Task<EntityT> GetEntityByIdAsync(int id)
        {
            // return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            throw new System.Exception();
        }

        public async Task<List<EntityT>> GetAllEntitiesAsync()
        {
            // return await GetAll().ToListAsync();
            throw new System.Exception();
        }
    }
}