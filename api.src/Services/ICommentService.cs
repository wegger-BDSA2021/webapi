using Data;
using Services;
using System.Threading.Tasks;

namespace api.src.Services
{
    interface ICommentService
    {
        Task<Result> GetComments();
        Task<Result> GetCommentById(int id);
        Task<Result> AddComment(Comment comment);
        Task<Result> DeleteComment(int id);
        Task<Result> UpdateComment(Comment comment);
    }
}
