using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IResourceRepository 
    {
        Task<Resource> GetEntityByIdAsync(int id);

        Task<List<Resource>> GetAllEntitiesAsync();
    }
}