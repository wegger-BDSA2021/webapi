using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IUserRepository
     {
        Task<User> GetUserByIdAsync(int id);

        Task<List<User>> GetAllUsersAsync();
    }
}