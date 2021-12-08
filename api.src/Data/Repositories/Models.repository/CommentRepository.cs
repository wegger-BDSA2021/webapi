using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static Data.Response;

namespace Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IWeggerContext _context;

        public CommentRepository(IWeggerContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyCollection<Comment>> GetComments()
        {
            var comments = await _context.Comments.ToListAsync();

            return comments.AsReadOnly();
        }

        public async Task<(Response Response, Comment comment)> GetCommentById(int commentId)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment is null)
                return (NotFound, null);

            return (OK, comment);
        }

        public async Task<(Response Response, Comment comment)> AddComment(Comment comment)
        {
            var result = await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return (Created, result.Entity);
        }

        public async Task<(Response Response, Comment comment)> DeleteComment(int commentId)
        {
            var result = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

            if (result != null)
            {
                _context.Comments.Remove(result);
                await _context.SaveChangesAsync();
                return (Deleted, result);
            }

            return (NotFound, null);
        }

        public async Task<(Response Response, Comment comment)> UpdateComment(Comment comment)
        {
            var result = await _context.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);

            if (result != null)
            {
                result.User = comment.User;
                result.Resource = comment.Resource;
                result.TimeOfComment = comment.TimeOfComment;
                result.Content = comment.Content;

                await _context.SaveChangesAsync();
                
                return (Updated, result);
            }

            return (NotFound, null);
        }
    }
}