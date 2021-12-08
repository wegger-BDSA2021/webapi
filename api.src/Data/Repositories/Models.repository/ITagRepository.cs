using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface ITagRepository
    {
        Task<(Response Response, TagDetailsDTO TagDetailsDTO)> CreateAsync(TagCreateDTO Tag);
        Task<Response> UpdateAsync(TagUpdateDTO tagUpdateDTO);
        Task<(Response Response,TagDetailsDTO TagDetailsDTO)> GetTagByIdAsync(int id);
        Task<IReadOnlyCollection<Tag>> GetAllTagsAsync();
        Task<Response> DeleteAsync(int id);

        // TODO : this needs to be implemented correctly, either with DTOs or just returing the name of the tag, like the method below 
        // public Task<IReadOnlyCollection<Tag>> GetAllTagsFormRepositoryAsync(Resource re);

        Task<IReadOnlyCollection<string>> GetAllTagsAsStringCollectionAsync();
    }
}