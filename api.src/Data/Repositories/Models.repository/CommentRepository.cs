using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
// using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IWeggerContext _context;
        public CommentRepository(IWeggerContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllEntitiesAsync()
        {
            // return await GetAll().ToListAsync();
            throw new System.Exception();
        }

        public Task<Comment> GetCommentByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}