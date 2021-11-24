using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface ITagRepository
    {
        public Task<(Response Response, int RatingId)> CreateAsync(Tag Tag);
        public Task<Response> UpdateAsync(Tag Tag , string newName);
        public Task<(Response Response,Tag)> GetTagByIdAsync(int id);
        public Task<IReadOnlyCollection<Tag>> GetAllTagsAsync();
        public Task<Response> DeleteAsync(int id);
        public Task<IReadOnlyCollection<Tag>> GetAllTagsFormRepositoryAsync(Resource re);
    }
}