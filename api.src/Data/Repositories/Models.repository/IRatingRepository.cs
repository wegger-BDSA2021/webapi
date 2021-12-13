using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IRatingRepository
    {
        public Task<(Response Response, RatingDetailsDTO RatingDetailsDTO)> CreateAsync(RatingCreateDTO Rating);
        public Task<Response> UpdateAsync(RatingUpdateDTO rating);
        public Task<Response> DeleteAsync(int id);
        public Task<(Response Response, Rating Rating)> ReadAsync(int id);
        public Task<(Response Response, Rating Rating)> ReadAsync(int userId, int resId);
        public Task<IReadOnlyCollection<Rating>> GetAllRatingFormRepositoryAsync(int reId);

        
    }
}