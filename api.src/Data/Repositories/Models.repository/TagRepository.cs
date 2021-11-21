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
            return await _context.Tags.FindAsync(id);
            // return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            // throw new System.Exception();
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            // return await GetAll().ToListAsync();
            throw new System.Exception();
        }
        //public async Task<List<Tag>> GetAllTagsFormRepositoryAsync(Resource re) => ( _context.Tags.Where(t => t.Resources == re).ToList<Tag>);
        public async Task<IReadOnlyCollection<Tag>> GetAllTagsFormRepositoryAsync(Resource re)
        {
            return ( await _context.Tags.Where(t => t.Resources == re).ToListAsync()).AsReadOnly();
        }
        /*{
            var tags = from t in _context.Tags
                        where(t => t.Resources = re)
                        select new TagDto;
            return await tags.GetAll().ToListAsync();

            // return await GetAll().ToListAsync();
            throw new System.Exception();
        }*/
    }
}