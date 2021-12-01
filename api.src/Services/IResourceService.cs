using System.Threading.Tasks;
using Data;
using Services;

public interface IResourceService
{
    public Task<Result> CreateAsync();
    public Task<Result> ReadAsync(int id);
}