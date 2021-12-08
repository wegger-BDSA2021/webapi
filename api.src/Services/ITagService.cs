using System.Threading.Tasks;
using Data;
using Services;

public interface ITagService
{
    public Task<Result> CreateAsync(TagCreateDTO tag);
    public Task<Result> ReadAsync(int id);
}