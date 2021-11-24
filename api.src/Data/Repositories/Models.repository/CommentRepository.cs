using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IWeggerContext _context;

        public CommentRepository(IWeggerContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetComments()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> GetCommentById(int commentId)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            var result = await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Comment> DeleteComment(int commentId)
        {
            var result = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);

            if (result != null)
            {
                _context.Comments.Remove(result);
                await _context.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<Comment> UpdateComment(Comment comment)
        {
            var result = await _context.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);

            if (result != null)
            {
                result.User = comment.User;
                result.Resource = comment.Resource;
                result.TimeOfComment = comment.TimeOfComment;
                result.Content = comment.Content;

                await _context.SaveChangesAsync();
                
                return result;
            }

            return null;
        }
    }
}