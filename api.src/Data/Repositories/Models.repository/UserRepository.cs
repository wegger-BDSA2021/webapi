using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
// using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IWeggerContext _context;
        public UserRepository(IWeggerContext context)
        {
            _context = context;
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}