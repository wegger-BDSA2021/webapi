using System;
using System.Threading.Tasks;
using Data;
using static Data.Response;
using Microsoft.AspNetCore.Mvc;
using ResourceBuilder;

namespace Services
{
    public class RatingService : IResourceService
    {
        // should validate input from the ResourceController
        // should use the parser component to create a new resource
        // contains all business logic 

        private IResourceRepository _repo;

        public RatingService(IResourceRepository repo)
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
                        Message = "No resource found with the given entity"
                    };
                
                case OK:
                    return new Result
                    {
                        Response = OK,
                        Message = $"Resource found at index {id}",
                        DTO = result.ResourceDetails
                    };

                default:
                    return new Result
                    {
                        Response = Conflict,
                        Message = "An error occured"
                    };
            }
        }

        public async Task<Result> CreateAsync(ResourceCreateDTOClient resource)
        {
            // validation of client input :
            // the actionFilter from MVC will take care of the properties of the DTOs
            // in the servicelayer, we will handle responses from the repos and similar, 
            // that can not be detected from the DTOs
            if (await _repo.LinkExistsAsync(resource.Url))
            {
                return new Result
                    {
                        Response = Conflict,
                        Message = "Another resource with the same URL has already been provided"
                    };
            }

            var validUrl = ValidateUrl(resource.Url);
            if (!validUrl)
            {
                return new Result
                    {
                        Response = BadRequest,
                        Message = "The provided URL is not valid"
                    };
            }

            var builder = new Builder(resource);
            var director = new Director(builder);

            try
            {
                var product = await director.Make();
                var created = await _repo.CreateAsync(product);
                if (created.Response is NotFound)
                {
                    return new Result
                        {
                            Response = Conflict, 
                            Message = "The user trying to create the resource does not exist in the current context"
                        };
                }

                return new Result
                    {
                        Response = Created,
                        Message = "A new resource was succesfully created", 
                        DTO = created.CreatedResource
                    };

            }
            catch (System.Exception)
            {
                return new Result
                    {
                        Response = InternalError,
                        Message = "Could not process the provided resource ... sorry about that. Try again later."
                    };
            }

        }

        private bool ValidateUrl(string _input)
        {
            Uri uriResult; 
            bool isValid = Uri.TryCreate(_input, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return isValid;
        }



    }
}