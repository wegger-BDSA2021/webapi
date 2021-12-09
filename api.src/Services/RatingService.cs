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
        public async Task<Result> UpdateAsync(TagUpdateDTO tag)
        {
            if (tag.Id < 0)
            {
                return new Result 
                {
                    Response = BadRequest,
                    Message = "Id can only be a positive integer"
                };
            }

            var result = await _repo.UpdateAsync(tag);

            switch (result)
            {
                case NotFound:
                    return new Result
                    {
                        Response = NotFound,
                        Message = "No tag found with the given entity"
                    };
                
                case OK:
                    // TODO: UPDATE
                    return new Result
                    {
                        Response = OK,
                        Message = $"Tag at index {tag.Id} has been updated form having the name {tag.Name} to have {tag.NewName}"
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
        public async Task<Result> CreateAsync(TagCreateDTO tag)
        {
            try
            {
                if (tag == null)
                {
                    return new Result
                    {
                       Response = BadRequest,
                       Message =  "No tag given"
                    };
                }

                var result = await _repo.CreateAsync(tag);
                if (result.Response == AllReadyExist){
                    return new Result
                    {
                       Response = AllReadyExist,
                       Message =  "The tag given allready exist in the database"
                    };
                }else{
                    return new Result
                    {
                       Response = Created,
                       Message =  "A new tag was succesfully created",
                       DTO = result.TagDetailsDTO
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
        public async Task<IReadOnlyCollection<TagDetailsDTO>> getAllTags()
        {
            return await _repo.GetAllTagsAsync();
        }

    }
}