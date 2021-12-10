using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IUserRepository
     {
        Task<(Response, User)> GetUserByIdAsync(string id);
        Task<(Response, string)> CreateUserAsync(string id);
        Task<Response> DeleteUserAsync(string id);
        Task<bool> UserExists(string id);

    }
}