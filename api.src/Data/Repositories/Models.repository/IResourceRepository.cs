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

        Task<IReadOnlyCollection<Resource>> GetAllDeprecatedAsync();

        Task<IReadOnlyCollection<Resource>> GetAllFromUserAsync(int userId);

        Task<IReadOnlyCollection<Resource>> GetAllFromDomainAsync(string domain);

        Task<IReadOnlyCollection<Resource>> GetAllWithTagsAsyc(ICollection<string> stringTags);

        Task<IReadOnlyCollection<Resource>> GetAllWithRatingInRangeAsync(int from, int to);
        
        Task<IReadOnlyCollection<Resource>> GetAllWhereTitleContainsAsync(string matcher);

        Task<(Response, double)> GetAverageRatingByIdAsync(int resourceId);

    }
}