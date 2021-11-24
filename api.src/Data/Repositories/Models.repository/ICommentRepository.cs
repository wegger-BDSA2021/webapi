using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetComments();
        Task<Comment> GetCommentById(int id);
        Task<Comment> AddComment(Comment comment);
        Task<Comment> DeleteComment(int id);
        Task<Comment> UpdateComment(Comment comment);
    }
}