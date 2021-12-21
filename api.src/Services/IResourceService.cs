using System.Collections.Generic;
using System.Threading.Tasks;
using Data;

namespace Services
{
    public interface IResourceService
    {
        Task<Result> CreateAsync(ResourceCreateDTOClient resource);
        Task<Result> ReadAsync(int id);
        Task<Result> ReadAllAsync();
        Task<Result> DeleteByIdAsync(int id);
        Task<Result> UpdateResourceAsync(ResourceUpdateDTO resource);
        Task<Result> GetAllResourcesFromUserAsync(string id);
        Task<Result> GetAllResourcesFromDomainAsync(string domain);
        Task<Result> GetAllResourcesWithinRangeAsync(int from, int to);
        Task<Result> GetAllResourcesWhereTitleContainsAsync(string matcher);
        Task<Result> GetAllResourcesMarkedDeprecatedAsync();
        Task<Result> GetAllArticleResourcesAsync();
        Task<Result> GetAllVideoResourcesAsync();
        Task<Result> GetAllFromOfficialDocumentationAsync();
        Task<Result> GetAllResourcesWithProvidedTags(ICollection<string> stringTags);

    }
}