using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IUserRepository
     {
        Task<(Response Response, User User)> GetUserByIdAsync(string id);
        Task<(Response Response, string Id)> CreateUserAsync(string id);
        Task<Response> DeleteUserAsync(string id);
        Task<bool> UserExists(string id);
    }
}