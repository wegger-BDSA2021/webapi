using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IResourceRepository 
    {
        Task<(Response Response, Resource Resource)> ReadAsync(int id);

        Task<IReadOnlyCollection<Resource>> ReadAllAsync();

        Task<(Response Response, int ResourceId)> CreateAsync(Resource resource);

        Task<Response> DeleteAsync(int id);

        Task<Response> UpdateAsync(Resource resource);

        Task<IReadOnlyCollection<Resource>> GetAllDeprecatedAsync();

        Task<IReadOnlyCollection<Resource>> GetAllFromUserAsync(int userId);

        Task<IReadOnlyCollection<Resource>> GetAllFromDomainAsync(string domain);

        Task<IReadOnlyCollection<Resource>> GetAllWithTagsAsyc(ICollection<Tag> tags);

        Task<IReadOnlyCollection<Resource>> GetAllWithRatingAsync(int rating);

        Task<IReadOnlyCollection<Resource>> GetAllWhereTitleContainsAsync(string matcher);

    }
}