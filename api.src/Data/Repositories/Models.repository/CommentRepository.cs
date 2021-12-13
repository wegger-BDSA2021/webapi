using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static Data.Response;
using Utils;

namespace Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IWeggerContext _context;

        public CommentRepository(IWeggerContext context)
        {
            _context = context;
        }

        /*public async Task<IReadOnlyCollection<CommentDTO>> GetComments()
        {
            var comments = await _context.Comments.ToListAsync();

            List<CommentDTO> commentsAsDto = new List<CommentDTO>();

            foreach (var item in comments)
            {
                commentsAsDto.Add(item.AsCommentDTO());
            }

            return commentsAsDto.AsReadOnly();
        }*/

        public async Task<IReadOnlyCollection<CommentDTO>> GetComments()
        => (await _context.Comments
                .Select(c =>
                    new CommentDTO(
                        c.Id,
                        c.UserId,
                        c.ResourceId,
                        c.TimeOfComment,
                        c.Content
                    ))
                .ToListAsync())
            .AsReadOnly();

        public async Task<(Response Response, CommentDetailsDTO comment)> GetCommentById(int commentId)
        {
            var _commentEntity = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

            if (_commentEntity is null)
                return (NotFound, null);

            var result = new CommentDetailsDTO(
                _commentEntity.Id,
                _commentEntity.UserId,
                _commentEntity.ResourceId,
                _commentEntity.TimeOfComment,
                _commentEntity.Content
            );

            return (OK, result); 
        }

        public async Task<(Response Response, CommentDetailsDTO comment)> AddComment(CommentCreateDTOServer comment)
        {
            var _commentEntity = new Comment
            {
                UserId = comment.UserId,
                ResourceId = comment.ResourceId,
                TimeOfComment = (System.DateTime)comment.TimeOfComment,
                Content = comment.Content
            };

            await _context.Comments.AddAsync(_commentEntity);

            await _context.SaveChangesAsync();

            CommentDetailsDTO result = new(
                _commentEntity.Id,
                _commentEntity.UserId,
                _commentEntity.ResourceId,
                _commentEntity.TimeOfComment,
                _commentEntity.Content
            );

            return (Created, result);
        }

        public async Task<Response> DeleteComment(int commentId)
        {
            var deletedComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

            if (deletedComment != null)
            {
                _context.Comments.Remove(deletedComment);
                await _context.SaveChangesAsync();
                return (Deleted);
            }

            return (NotFound);
        }

        public async Task<Response> UpdateComment(CommentUpdateDTO comment)
        {
            var result = await _context.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);

            if (result != null)
            {
                result.UserId = comment.UserId;
                result.ResourceId = (int)comment.ResourceId;
                result.TimeOfComment = (System.DateTime)comment.TimeOfComment;
                result.Content = comment.Content;

                await _context.SaveChangesAsync();
                
                return (Updated);
            }

            return (NotFound);
        }
    }
}