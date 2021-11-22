using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using static Data.Response;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Rating> GetRatingByIdAsync(int id)
        {
            // return await GetAll().FirstOrDefaultAsync(x => x.Id == id);
            throw new System.Exception();
        }

        public async Task<List<Rating>> GetAllRatingsAsync()
        {
            // return await GetAll().ToListAsync();
            throw new System.Exception();
        }
        public async Task<IReadOnlyCollection<Rating>> GetAllRatingFormRepositoryAsync(Resource re)
        {
            return ( await _context.Ratings.Where(r => r.Resource == re).ToListAsync()).AsReadOnly();
        }
        
    }
}