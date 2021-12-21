using System;
using System.Threading.Tasks;
using Data;
using static Data.Response;

namespace Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _repo;

        public RatingService(IRatingRepository repo)
        {
            _repo = repo;
        }
        
        public async Task<Result> ReadAsync(int id)
        {
            if (id < 0)
            {
                return new Result 
                {
                    Response = BadRequest,
                    Message = "Id can only be a positive integer"
                };
            }

            var result = await _repo.ReadAsync(id);

            switch (result.Response)
            {
                case NotFound:
                    return new Result
                    {
                        Response = NotFound,
                        Message = "No Rating found with the given entity"
                    };
                
                case OK:
                    return new Result
                    {
                        Response = OK,
                        Message = $"Rating found at index {id}",
                        DTO = result.RatingDetailsDTO
                    };

                default:
                    return new Result
                    {
                        Response = Conflict,
                        Message = "An error occured"
                    };
            }
        }
        public async Task<Result> ReadAsync(string userId, int resId)
        {
            if (resId < 0)
            {
                return new Result 
                {
                    Response = BadRequest,
                    Message = "Id can only be a positive integer"
                };
            }


            var result = await _repo.ReadAsync(userId,resId);

            switch (result.Response)
            {
                case NotFound:
                    return new Result
                    {
                        Response = NotFound,
                        Message = "No Rating found with the given entity"
                    };
                
                case OK:
                    return new Result
                    {
                        Response = OK,
                        Message = $"Rating found at index {result.RatingDetailsDTO.Id}",
                        DTO = result.RatingDetailsDTO
                    };

                default:
                    return new Result
                    {
                        Response = Conflict,
                        Message = "An error occured"
                    };
            }
        }
        public async Task<Result> ReadAllRatingFormRepositoryAsync(int id)
        {
            if (id < 0)
            {
                return new Result 
                {
                    Response = BadRequest,
                    Message = "Id can only be a positive integer"
                };
            }

            var result = await _repo.GetAllRatingFormResourceAsync(id);
            
            return new Result 
                {
                    Response = OK,
                    DTO = result
                };
            
        }
        public async Task<Result> UpdateAsync(RatingUpdateDTO ratingUpdate)
        {
            if (ratingUpdate.Id < 0)
            {
                return new Result 
                {
                    Response = BadRequest,
                    Message = "Id can only be a positive integer"
                };
            }

            var result = await _repo.UpdateAsync(ratingUpdate);

            switch (result)
            {
                case NotFound:
                    return new Result
                    {
                        Response = NotFound,
                        Message = "No Rating found with the given entity"
                    };
                
                case Updated:
                    return new Result
                    {
                        Response = Updated,
                        Message = $"Rating at index {ratingUpdate.Id} has been updated form having the rating {ratingUpdate.UpdatedRating} to have {ratingUpdate.UpdatedRating}"
                    };
                case Conflict:
                    return new Result
                    {
                        Response = BadRequest,
                        Message = "Invalid Rating"
                    };
                default:
                    return new Result
                    {
                        Response = Conflict,
                        Message = "An error occured"
                    };
            }
            

        }

        public async Task<Result> Delete(int id)
        {
            var result = await _repo.DeleteAsync(id);
            switch (result)
            {
                case NotFound:
                    return new Result
                    {
                        Response = NotFound,
                        Message = "No Rating found with the given entity"
                    };
                
                case Deleted:
                    return new Result
                    {
                        Response = Deleted,
                        Message = $"Rating found at index {id}"
                    };

                default:
                    return new Result
                    {
                        Response = Conflict,
                        Message = "An error occured"
                    };
            }
        
            
            
        }
        public async Task<Result> CreateAsync(RatingCreateDTO rating)
        {
            try
            {
                if (rating == null)
                {
                    return new Result
                    {
                       Response = BadRequest,
                       Message =  "No Rating given"
                    };
                }

                var result = await _repo.CreateAsync(rating);
                switch (result.Response)
                {
                    case Conflict:
                        return new Result
                        {
                           Response = BadRequest,
                           Message =  "Invalid Rating"
                        };
                    case Created:
                        return new Result
                        {
                           Response = Created,
                           Message =  "A new Rating was succesfully created",
                           DTO = result.RatingDetailsDTO
                        };
                    default:
                        return new Result
                        {
                            Response = Conflict,
                            Message = "An error occured"
                        };
                }

                

            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e);
                return new Result
                    {
                        Response = InternalError,
                        Message = "Could not process the provided Rating ... sorry about that. Try again later."
                    };
            }

        }
    }
}