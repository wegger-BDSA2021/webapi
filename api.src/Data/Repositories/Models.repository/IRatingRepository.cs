using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IRatingRepository
    {
        public Task<Tag> GetRatingByIdAsync(int id);
        public Task<List<Tag>> GetAllRatingsAsync();
    }
}