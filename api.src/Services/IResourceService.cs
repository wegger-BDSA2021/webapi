using System.Threading.Tasks;
using Data;
using Services;

public interface IResourceService
{
    Task<Result> CreateAsync(ResourceCreateDTOClient resource);
    Task<Result> ReadAsync(int id);
    Task<Result> ReadAllAsync();
    Task<Result> DeleteByIdAsync(int id);
    Task<Result> UpdateResourceAsync(ResourceUpdateDTO resource);
}