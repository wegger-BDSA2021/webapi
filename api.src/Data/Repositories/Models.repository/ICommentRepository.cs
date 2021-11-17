using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface ICommentRepository
    {
        Task<Comment> GetCommentByIdAsync(int id);

        Task<List<Comment>> GetAllEntitiesAsync();
    }
}