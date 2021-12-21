using System.Threading.Tasks;
using Data;

namespace Services
{
    public interface ITagService
    {
        Task<Result> CreateAsync(TagCreateDTO tag);
        Task<Result> ReadAsync(int id);
        Task<Result> UpdateAsync(TagUpdateDTO tag);
        Task<Result> Delete(int id);
        Task<Result> getAllTags();
    }
}