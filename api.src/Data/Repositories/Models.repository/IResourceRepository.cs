using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IResourceRepository
    {
        Task<(Response Response, ResourceDetailsDTO ResourceDetails)> ReadAsync(int id);
        Task<IReadOnlyCollection<ResourceDTO>> ReadAllAsync();
        Task<(Response Response, ResourceDetailsDTO CreatedResource)> CreateAsync(ResourceCreateDTOServer resource);
        Task<Response> DeleteAsync(int id);
        Task<Response> UpdateAsync(ResourceUpdateDTO resource);
        Task<IReadOnlyCollection<ResourceDTO>> GetAllDeprecatedAsync();
        Task<IReadOnlyCollection<ResourceDTO>> GetAllVideosAsync();
        Task<IReadOnlyCollection<ResourceDTO>> GetAllArticlesAsync();
        Task<IReadOnlyCollection<ResourceDTO>> GetAllFromOfficialDocumentaionAsync();
        Task<IReadOnlyCollection<ResourceDTO>> GetAllFromUserAsync(string userId);
        Task<IReadOnlyCollection<ResourceDTO>> GetAllFromDomainAsync(string domain);
        Task<IReadOnlyCollection<ResourceDTO>> GetAllWithTagsAsyc(ICollection<string> stringTags);
        Task<IReadOnlyCollection<ResourceDTO>> GetAllWithRatingInRangeAsync(int from, int to);
        Task<IReadOnlyCollection<ResourceDTO>> GetAllWhereTitleContainsAsync(string matcher);
        Task<(Response Response, double Average)> GetAverageRatingByIdAsync(int resourceId);
        Task<bool> LinkExistsAsync(string url);
    }
}