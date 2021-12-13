using System;
using System.Threading.Tasks;
using Data;
using static Data.Response;
using Microsoft.AspNetCore.Mvc;
using ResourceBuilder;
using System.Linq;
using System.Collections.Generic;

namespace Services
{
    public class RatingService : IRatingService
    {
        // should validate input from the ResourceController
        // should use the parser component to create a new resource
        // contains all business logic 

        private IRatingRepository _repo;

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
                        Message = "No tag found with the given entity"
                    };
                
                case OK:
                    return new Result
                    {
                        Response = OK,
                        Message = $"Tag found at index {id}",
                        DTO = result
                    };

                default:
                    return new Result
                    {
                        Response = Conflict,
                        Message = "An error occured"
                    };
            }
        }
        public async Task<Result> ReadAsync(int userId, int resId)
        {
            if (userId < 0 || resId < 0)
            {
                return new Result 
                {
                    Response = BadRequest,
                    Message = "Id can only be a positive integer"
                };
            }


            var result = await _repo.ReadAsync(userId);

            switch (result.Response)
            {
                case NotFound:
                    return new Result
                    {
                        Response = NotFound,
                        Message = "No tag found with the given entity"
                    };
                
                case OK:
                    return new Result
                    {
                        Response = OK,
                        Message = $"Tag found at index {result.Rating.Id}",
                        DTO = result
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
                
                case OK:
                    // TODO: UPDATE
                    return new Result
                    {
                        Response = OK,
                        Message = $"Rating at index {ratingUpdate.Id} has been updated form having the rating {ratingUpdate.UpdatedRating} to have {ratingUpdate.UpdatedRating}"
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
                        Message = "No tag found with the given entity"
                    };
                
                case Deleted:
                    return new Result
                    {
                        Response = Deleted,
                        Message = $"Tag found at index {id}"
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
                       Message =  "No tag given"
                    };
                }

                var result = await _repo.CreateAsync(rating);

                if (result.Response == Conflict){
                    return new Result
                    {
                       Response = BadRequest,
                       Message =  "Invalid rating"
                    };
                }else{
                    return new Result
                    {
                       Response = Created,
                       Message =  "A new tag was succesfully created",
                       DTO = result.RatingDetailsDTO
                    };
                }

                

            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e);
                return new Result
                    {
                        Response = InternalError,
                        Message = "Could not process the provided resource ... sorry about that. Try again later."
                    };
            }

        }
    }
}