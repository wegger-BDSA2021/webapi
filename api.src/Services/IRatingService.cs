using System.Threading.Tasks;
using Data;

namespace Services
{
    public interface IRatingService
    {
        Task<Result> CreateAsync(RatingCreateDTO rating);
        Task<Result> ReadAsync(int id);
        Task<Result> UpdateAsync(RatingUpdateDTO ratingUpdate);
        Task<Result> Delete(int id);
        Task<Result> ReadAllRatingFormRepositoryAsync(int resId);
    }
}