using System.Threading.Tasks;
using Data;
using static Data.Response;

namespace Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repo;

        public TagService(ITagRepository repo)
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

            var result = await _repo.GetTagByIdAsync(id);

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
                        DTO = result.TagDetailsDTO
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
                
                case BadRequest:
                    return new Result
                    {
                        Response = BadRequest,
                        Message = $"There already exists a tag with name {tag.NewName}"
                    };

                case Updated:
                    return new Result
                    {
                        Response = Updated,
                        Message = $"Tag at index {tag.Id} has been updated to have name {tag.NewName}"
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
                       Message =  "The tag given already exist in the database"
                    };
                }else{
                    return new Result
                    {
                       Response = Created,
                       Message =  "A new tag was successfully created",
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
        
        public async Task<Result> getAllTags()
        {
            var tags = await _repo.GetAllTagsAsync();
            return new Result
                {
                    Response = OK,
                    DTO = tags
                };
        }

    }
}