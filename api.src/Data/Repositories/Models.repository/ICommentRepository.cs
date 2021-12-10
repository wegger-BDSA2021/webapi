using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface ICommentRepository
    {
        Task<IReadOnlyCollection<CommentDTO>> GetComments();
        Task<(Response Response, CommentDetailsDTO comment)> GetCommentById(int id);
        Task<(Response Response, CommentDetailsDTO comment)> AddComment(CommentCreateDTOServer comment);
        Task<Response> DeleteComment(int id);
        Task<Response> UpdateComment(CommentUpdateDTO comment);
    }
}