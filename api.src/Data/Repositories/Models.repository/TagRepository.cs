using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using static Data.Response;
using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore;

//using Utils;
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
        public async Task<(Response Response, int RatingId)> CreateAsync(Tag Tag)
        {
            var entity = new Tag
            {
                Name = Tag.Name,
                Resources = Tag.Resources,
            };
            await _context.Tags.AddAsync(entity);
            await _context.SaveChangesAsync();

            return (Created, entity.Id);
        }
        public async Task<Response> UpdateAsync(Tag Tag , string newName)
        {
            var entity = await _context.Tags.FindAsync(Tag.Id);
            if (entity == null)
                return NotFound;
            
            entity.Name = newName;

            await _context.SaveChangesAsync();

            return Updated;
        }

        public async Task<(Response Response,Tag Tag)> GetTagByIdAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag is null)
                return (NotFound, null);

            return (OK, tag);

            // return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            // throw new System.Exception();
        }

        public async Task<IReadOnlyCollection<Tag>> GetAllTagsAsync()
        {
            // return await GetAll().ToListAsync();
            return (await _context.Tags.ToListAsync()).AsReadOnly();
        }
        //public async Task<List<Tag>> GetAllTagsFormRepositoryAsync(Resource re) => ( _context.Tags.Where(t => t.Resources == re).ToList<Tag>);
        //public async Task<List<Tag>> GetAllTagsFormRepositoryAsync(Resource re) => ( ( await _context.Tags.Where(t => t.Resources == re).ToListAsync()).AsReadOnly());
        public async Task<Response> DeleteAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag is null)
                return NotFound;

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return Deleted;
        }

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