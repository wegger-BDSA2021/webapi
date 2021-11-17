using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface ITagRepository
    {
        public Task<Tag> GetTagByIdAsync(int id);
        public Task<List<Tag>> GetAllTagsAsync();
    }
}