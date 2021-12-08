using System.Threading.Tasks;
using Data;
using Services;

public interface IRatingService
{
    public Task<Result> CreateAsync(ResourceCreateDTOClient resource);
    public Task<Result> ReadAsync(int id);
}