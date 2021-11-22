using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IRatingRepository
    {
        public Task<Rating> GetRatingByIdAsync(int id);
        public Task<List<Rating>> GetAllRatingsAsync();
        public Task<IReadOnlyCollection<Rating>> GetAllRatingFormRepositoryAsync(Resource re);

        
    }
}