using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
// using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class TagRepository : ITagRepository
    {
        private readonly IWeggerContext _context;
        public TagRepository(IWeggerContext context)
        {
            _context = context;
        }

        public async Task<Tag> GetTagByIdAsync(int id)
        {
            // return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            throw new System.Exception();
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            // return await GetAll().ToListAsync();
            throw new System.Exception();
        }
    }
}