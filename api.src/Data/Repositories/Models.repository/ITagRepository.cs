using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface ITagRepository
    {
        Task<(Response Response, TagDetailsDTO TagDetailsDTO)> CreateAsync(TagCreateDTO Tag);
        Task<Response> UpdateAsync(TagUpdateDTO tagUpdateDTO);
        Task<(Response Response,TagDetailsDTO TagDetailsDTO)> GetTagByIdAsync(int id);
        Task<IReadOnlyCollection<TagDetailsDTO>> GetAllTagsAsync();
        Task<Response> DeleteAsync(int id);
        Task<IReadOnlyCollection<string>> GetAllTagsAsStringCollectionAsync();
    }
}