using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using static Data.Response;

namespace Data
{
    public class RatingRepository : IRatingRepository
    {
        private readonly IWeggerContext _context;
        public RatingRepository(IWeggerContext context)
        {
            _context = context;
        }
        
        public async Task<(Response Response, RatingDetailsDTO RatingDetailsDTO)> CreateAsync(RatingCreateDTO Rating)
        {
            if (Rating.Rated > 5 || Rating.Rated < 0)
                return (Conflict, null);

            var entity = new Rating
            {
                User = _context.Users.First(u => u.Id == Rating.UserId),
                Resource = _context.Resources.First(u => u.Id == Rating.ResourceId),
                Rated = Rating.Rated,
            };

            await _context.Ratings.AddAsync(entity);
            await _context.SaveChangesAsync();

            var result = new RatingDetailsDTO(
                entity.Id,
                entity.UserId,
                entity.ResourceId,
                entity.Rated
            );

            return (Created, result);
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

        public async Task<Response> UpdateAsync(RatingUpdateDTO Rating)
        {
            var entity = await _context.Ratings.FindAsync(Rating.Id);
            if (entity == null)
                return NotFound;
            if (Rating.UpdatedRating > 5 || Rating.UpdatedRating < 0)
                return Conflict;
            
            
            entity.Rated = Rating.UpdatedRating;

            await _context.SaveChangesAsync();

            return Updated;
        }

        public async Task<(Response Response, RatingDetailsDTO RatingDetailsDTO)> ReadAsync(int id)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(r => r.Id == id);
            if (rating is null)
                return (NotFound, null);

            var result = new RatingDetailsDTO(
                rating.Id,
                rating.UserId,
                rating.ResourceId,
                rating.Rated
            );

            return (OK, result);
        }

        public async Task<(Response Response, RatingDetailsDTO RatingDetailsDTO)> ReadAsync(string userId,int resId)
        {
            var ratingM = await _context.Ratings.Where(r => r.User.Id == userId).ToListAsync();
            if (ratingM is null)
                return (NotFound, null);

            Rating ratingS = null;
            
            try
            {
                ratingS = ratingM.Where(r => r.Resource.Id == resId).First(); 
            }
            catch (Exception)
            {
                return (NotFound, null);
            }
            var result = new RatingDetailsDTO(
                ratingS.Id,
                ratingS.UserId,
                ratingS.ResourceId,
                ratingS.Rated
            );
            return (OK, result);
        }

        public async Task<IReadOnlyCollection<Rating>> GetAllRatingFormResourceAsync(int reId)
        {
            return ( await _context.Ratings.Where(r => r.Resource.Id == reId).ToListAsync()).AsReadOnly();
        }
        
    }
}