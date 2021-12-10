using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static Data.Response;

namespace Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IWeggerContext _context;
        public UserRepository(IWeggerContext context)
        {
            _context = context;
        }

        public async Task<(Response, User)> GetUserByIdAsync(string id)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userEntity is null)
                return (NotFound, null);

            return (OK, userEntity);
        }

        public async Task<bool> UserExists(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null)
                return false;

            return true;
        }

    }
}