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

        public async Task<(Response Response, User User)> GetUserByIdAsync(string id)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userEntity is null)
                return (NotFound, null);

            return (OK, userEntity);
        }

        public async Task<(Response Response, string Id)> CreateUserAsync(string id)
        {
            var exists = await UserExists(id);
            if (exists)
                return (BadRequest, null);

            var entity = new User { Id = id };

            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();

            return (Created, entity.Id);
        }

        public async Task<Response> DeleteUserAsync(string id)
        {
            var userEntity = await GetUserByIdAsync(id);
            if (userEntity.Response == NotFound)
                return NotFound;

            _context.Users.Remove(userEntity.User);
            await _context.SaveChangesAsync();

            return Deleted;
        }

        public async Task<bool> UserExists(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                return false;

            return true;
        }

    }
}