using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IRatingRepository
    {
        public Task<(Response Response, int RatingId)> CreateAsync(Rating Rating);
        public Task<Response> UpdateAsync(Rating Rating, int newRating);
        public Task<Response> DeleteAsync(int id);
        public Task<(Response Response, Rating Rating)> ReadAsync(int id);
        public Task<(Response Response, Rating Rating)> ReadAsync(int userId, int resId);
        public Task<IReadOnlyCollection<Rating>> GetAllRatingFormRepositoryAsync(Resource re);

        
    }
}