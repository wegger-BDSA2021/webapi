using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface ICommentRepository
    {
        Task<IReadOnlyCollection<Comment>> GetComments();
        Task<(Response Response, Comment comment)> GetCommentById(int id);
        Task<(Response Response, Comment comment)> AddComment(Comment comment);
        Task<(Response Response, Comment comment)> DeleteComment(int id);
        Task<(Response Response, Comment comment)> UpdateComment(Comment comment);
    }
}