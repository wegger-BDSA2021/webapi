using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IResourceRepository 
    {
        Task<(Response Response, ResourceDetailsDTO ResourceDetails)> ReadAsync(int id);

        Task<IReadOnlyCollection<ResourceDTO>> ReadAllAsync();

        Task<(Response Response, ResourceDetailsDTO CreatedResource)> CreateAsync(ResourceCreateDTO resource);

        Task<Response> DeleteAsync(int id);

        Task<Response> UpdateAsync(ResourceUpdateDTO resource);

        Task<IReadOnlyCollection<ResourceDTO>> GetAllDeprecatedAsync();

        Task<IReadOnlyCollection<ResourceDTO>> GetAllFromUserAsync(int userId);

        Task<IReadOnlyCollection<ResourceDTO>> GetAllFromDomainAsync(string domain);

        Task<IReadOnlyCollection<ResourceDTO>> GetAllWithTagsAsyc(ICollection<string> stringTags);

        Task<IReadOnlyCollection<ResourceDTO>> GetAllWithRatingInRangeAsync(int from, int to);
        
        Task<IReadOnlyCollection<ResourceDTO>> GetAllWhereTitleContainsAsync(string matcher);

        Task<(Response Response, double Average)> GetAverageRatingByIdAsync(int resourceId);

    }
}