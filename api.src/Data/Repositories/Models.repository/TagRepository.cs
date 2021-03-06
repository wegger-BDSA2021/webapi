using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static Data.Response;

namespace Data
{
    public class TagRepository : ITagRepository
    {
        private readonly IWeggerContext _context;
        public TagRepository(IWeggerContext context)
        {
            _context = context;
        }

        public async Task<(Response Response, TagDetailsDTO TagDetailsDTO)> CreateAsync(TagCreateDTO Tag)
        {
            var exists = await _context.Tags.FirstOrDefaultAsync(t => t.Name == Tag.Name);
            if (exists != null)
            {
                return (AllReadyExist, null);
            }

            var entity = new Tag
            {
                Name = Tag.Name,
            };

            await _context.Tags.AddAsync(entity);
            await _context.SaveChangesAsync();

            var result = new TagDetailsDTO(
                entity.Id,
                entity.Name,
                new List<string>()
            );

            return (Created, result);
        }

        public async Task<Response> UpdateAsync(TagUpdateDTO tagUpdateDTO)
        {
            var entity = await _context.Tags.FindAsync(tagUpdateDTO.Id);
            if (entity == null)
                return NotFound;

            var nameExists = await _context.Tags.Where(t => t.Name.ToLower().Trim() == tagUpdateDTO.NewName.ToLower().Trim()).FirstOrDefaultAsync();
            if (nameExists != null)
                return Conflict;
            
            entity.Name = tagUpdateDTO.NewName;

            await _context.SaveChangesAsync();

            return Updated;
        }

        public async Task<(Response Response,TagDetailsDTO TagDetailsDTO)> GetTagByIdAsync(int id)
        {
            var tag = await _context.Tags.Include(t => t.Resources).FirstOrDefaultAsync(t => t.Id == id);
            if (tag is null)
                return (NotFound, null);

            var result = new TagDetailsDTO(
                tag.Id,
                tag.Name,
                tag.Resources.Select(r => r.Title).DefaultIfEmpty().ToList()
            );

            return (OK, result);            
        }

        public async Task<IReadOnlyCollection<TagDetailsDTO>> GetAllTagsAsync()
        {
            return (await _context.Tags
                .Include(t => t.Resources)
                    .Select(t => 
                        new TagDetailsDTO(
                            t.Id,
                            t.Name,
                            t.Resources.Select(r => r.Title).ToList()
                        ))
                    .ToListAsync())
                .AsReadOnly();
        }
        

        public async Task<IReadOnlyCollection<string>> GetAllTagsAsStringCollectionAsync()
            => ( await _context.Tags.Select(t => t.Name).ToListAsync()).AsReadOnly();
        

        public async Task<Response> DeleteAsync(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag is null)
                return NotFound;

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();

            return Deleted;
        }

    }
}