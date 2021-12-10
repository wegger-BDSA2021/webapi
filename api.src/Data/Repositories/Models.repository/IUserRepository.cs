using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IUserRepository
     {
        Task<User> GetUserByIdAsync(string id);

        Task<List<User>> GetAllUsersAsync();
    }
}