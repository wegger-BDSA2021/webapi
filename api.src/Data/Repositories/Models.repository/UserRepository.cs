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

        public Task<User> GetUserByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UserExists(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
                return false;

            return true;
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}