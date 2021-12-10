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
        public async Task<(Response Response, int RatingId)> CreateAsync(Rating Rating)
        {
            if (Rating.Rated > 5 || Rating.Rated > 0)
                return (Conflict, -1);
            var entity = new Rating
            {
                User = Rating.User,
                Resource = Rating.Resource,
                Rated = Rating.Rated,
            };
            await _context.Ratings.AddAsync(entity);
            await _context.SaveChangesAsync();

            return (Created, entity.Id);
        }
        public async Task<Response> DeleteAsync(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating is null)
                return NotFound;

            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();

            return Deleted;
        }
        public async Task<Response> UpdateAsync(Rating Rating, int newRating)
        {
            var entity = await _context.Ratings.FindAsync(Rating.Id);
            if (entity == null)
                return NotFound;
            if (newRating > 5 || newRating > 0)
                return Conflict;
            
            
            //entity.User = Rating.User; //maybe remove
            //entity.Resource = Rating.Resource; //maybe remove
            entity.Rated = newRating;

            await _context.SaveChangesAsync();

            return Updated;
        }
        public async Task<(Response Response, Rating Rating)> ReadAsync(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating is null)
                return (NotFound, null);

            return (OK, rating);
        }
        public async Task<(Response Response, Rating Rating)> ReadAsync(string userId,int resId)
        {
            var ratingM = await _context.Ratings.Where(r => r.User.Id == userId).ToListAsync();
            var ratingS = ratingM.Where(r => r.Resource.Id == resId).First(); //might not give null on fail. test
            if (ratingS is null)
                return (NotFound, null);

            return (OK, ratingS);
        }
        public async Task<IReadOnlyCollection<Rating>> GetAllRatingFormRepositoryAsync(Resource re)
        {
            return ( await _context.Ratings.Where(r => r.Resource == re).ToListAsync()).AsReadOnly();
        }
        
    }
}