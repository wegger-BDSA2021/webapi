using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IEntityRepository : IRepository<EntityT>
    {
        Task<EntityT> GetEntityByIdAsync(int id);

        Task<List<EntityT>> GetAllEntitiesAsync();
    }
}