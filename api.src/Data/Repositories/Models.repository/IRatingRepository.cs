using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data
{
    public interface IRatingRepository
    {
        Task<(Response Response, RatingDetailsDTO RatingDetailsDTO)> CreateAsync(RatingCreateDTO Rating);
        Task<Response> UpdateAsync(RatingUpdateDTO rating);
        Task<Response> DeleteAsync(int id);
        Task<(Response Response, Rating Rating)> ReadAsync(int id);
        // public Task<(Response Response, Rating Rating)> ReadAsync(int userId, int resId);
        Task<IReadOnlyCollection<Rating>> GetAllRatingFormResourceAsync(int reId);


        
    }
}