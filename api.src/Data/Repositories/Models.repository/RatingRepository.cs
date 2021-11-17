using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
// using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class RatingRepository : IRatingRepository
    {
        private readonly IWeggerContext _context;
        public RatingRepository(IWeggerContext context)
        {
            _context = context;
        }

        public async Task<Tag> GetRatingByIdAsync(int id)
        {
            // return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            throw new System.Exception();
        }

        public async Task<List<Tag>> GetAllRatingsAsync()
        {
            // return await GetAll().ToListAsync();
            throw new System.Exception();
        }
    }
}