using System.Threading.Tasks;
using Data;
using static Data.Response;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task<Result> CreateAsync(string id)
        {
            if (id.Trim().Length == 0)
                return new Result 
                    {
                        Response = BadRequest,
                        Message = "Id can not be the empty string"
                    };

            var resultFromRepo = await _repo.CreateUserAsync(id);

            if (resultFromRepo.Response == BadRequest)
                return new Result 
                    {
                        Response = BadRequest,
                        Message = $"User with id {id} already exists"
                    }; 

            return new Result 
                    {
                        Response = Created,
                        Message = $"User with id {id} has been created",
                        DTO = resultFromRepo.Id

                    };
        }

        public async Task<Result> DeleteAsync(string id)
        {
            if (id.Trim().Length == 0)
                return new Result 
                    {
                        Response = BadRequest,
                        Message = "Id can not be the empty string"
                    };

            var responseFromRepo = await _repo.DeleteUserAsync(id);

            if (responseFromRepo == NotFound)
                return new Result 
                    {
                        Response = NotFound,
                        Message = $"User with id {id} does not exists"
                    }; 

            return new Result 
                    {
                        Response = Deleted,
                        Message = $"User with id {id} has been deleted"
                    };
        }
    }

}