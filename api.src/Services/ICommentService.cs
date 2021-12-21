using Data;
using System.Threading.Tasks;

namespace Services
{
    public interface ICommentService
    {
        Task<Result> GetComments();
        Task<Result> GetCommentById(int id);
        Task<Result> AddComment(CommentCreateDTOServer comment);
        Task<Result> DeleteComment(int id);
        Task<Result> UpdateComment(CommentUpdateDTO comment);
    }
}
