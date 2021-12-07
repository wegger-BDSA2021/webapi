using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
                if (user is null)
                    return null;

                return user;
        }

        public async Task<IReadOnlyList<User>> GetAllUsersAsync()
        {
            return (await _context.Users.ToListAsync()).AsReadOnly();
        }
    }
}