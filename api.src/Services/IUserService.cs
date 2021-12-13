using System.Threading.Tasks;

namespace Services
{
    public interface IUserService
    {
        Task<Result> CreateAsync(string id);
        Task<Result> DeleteAsync(string id);
    }
}