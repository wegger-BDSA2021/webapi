using System.Threading.Tasks;
using Data;
using Services;
using System;
using static Data.Response;
using Microsoft.AspNetCore.Mvc;
using ResourceBuilder;
using System.Linq;
using System.Collections.Generic;

public interface IRatingService
{
    public Task<Result> CreateAsync(RatingCreateDTO rating);
    public Task<Result> ReadAsync(int id);
    // public Task<Result> ReadAsync(int userId, int resId);
    public Task<Result> UpdateAsync(RatingUpdateDTO ratingUpdate);
    public Task<Result> Delete(int id);
    public Task<IReadOnlyCollection<Rating>> ReadAllRatingFormRepositoryAsync(int resId);

}