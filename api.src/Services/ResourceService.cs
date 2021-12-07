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
    public class ResourceService : IResourceService
    {
        private IResourceRepository _resourceRepo;
        private ITagRepository _tagRepo;
        // private IUserRepository _userRepo;


        public ResourceService(IResourceRepository repo, ITagRepository tagRepo)
        {
            _resourceRepo = repo;
            _tagRepo = tagRepo;
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

            var result = await _resourceRepo.ReadAsync(id);

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
            if (await _resourceRepo.LinkExistsAsync(resource.Url))
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

            var queryTerms = await _tagRepo.GetAllTagsAsStringCollectionAsync();

            var builder = new Builder(resource, queryTerms.ToList());
            var director = new Director(builder);

            try
            {
                var product = await director.Make();
                var created = await _resourceRepo.CreateAsync(product);
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

        private bool ValidateUrl(string _input)
        {
            Uri uriResult; 
            bool isValid = Uri.TryCreate(_input, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return isValid;
        }

        public async Task<Result> ReadAllAsync()
        {
            var collection = await _resourceRepo.ReadAllAsync();
            var result = new Result 
                {
                    Response = OK,
                    DTO = collection
                };
            return result;
        }

        public async Task<Result> DeleteByIdAsync(int id)
        {
            var response = await _resourceRepo.DeleteAsync(id);
            if (response == NotFound)
                return new Result
                    {
                        Response = NotFound,
                        Message = $"No resource found with id {id}"
                    };
            
            return new Result 
                {
                    Response = Deleted,
                    Message = $"Resource with id {id} has succesfully benn deleted"
                };
        }

        // TODO : cascading update, and reading new tags ...
        public async Task<Result> UpdateResourceAsync(ResourceUpdateDTO resource)
        {
            var response = await _resourceRepo.UpdateAsync(resource);
            if (response == NotFound)
                return new Result
                    {
                        Response = NotFound,
                        Message = $"No resource found with the id {resource.Id}"
                    };
            return new Result
                {
                    Response = Updated,
                    Message = $"Resource with id {resource.Id} has succefully been updated"
                };
        }

        public async Task<Result> GetAllResourcesFromUserAsync(int id)
        {
            if (id < 0)
            {
                return new Result 
                    {
                        Response = BadRequest,
                        Message = "Id can only be a positive integer"
                    };
            }

            // TODO : check if user exists via user_repo

            var collection = await _resourceRepo.GetAllFromUserAsync(id);
            return new Result
                {
                    Response = OK, 
                    Message = $"Collection with {collection.Count()} resources found from user with id {id}",
                    DTO = collection
                };
        }

        public async Task<Result> GetAllResourcesFromDomainAsync(string domain)
        {
            var collection = await _resourceRepo.GetAllFromDomainAsync(domain);
            return new Result
                {
                    Response = OK, 
                    Message = $"Collection with {collection.Count()} resources found from {domain}",
                    DTO = collection
                };
        }

        public async Task<Result> GetAllResourcesWithinRangeAsync(int from, int to)
        {
            var collection = await _resourceRepo.GetAllWithRatingInRangeAsync(from, to);
            return new Result
                {
                    Response = OK, 
                    Message = $"Collection with {collection.Count()} resources found with average rating between {from} and {to}",
                    DTO = collection
                };
        }

        public async Task<Result> GetAllResourcesWhereTitleContainsAsync(string matcher)
        {
            var collection = await _resourceRepo.GetAllWhereTitleContainsAsync(matcher);
            return new Result
                {
                    Response = OK, 
                    Message = $"Collection with {collection.Count()} resources found where title contains '{matcher}'",
                    DTO = collection
                };
        }

        public async Task<Result> GetAllResourcesMarkedDeprecatedAsync()
        {
            var collection = await _resourceRepo.GetAllDeprecatedAsync();
            return new Result
                {
                    Response = OK, 
                    Message = $"Collection with {collection.Count()} resources found, that has been marked as deprecated",
                    DTO = collection
                };
        }

        public async Task<Result> GetAllArticleResourcesAsync()
        {
            var collection = await _resourceRepo.GetAllArticlesAsync();
            return new Result
                {
                    Response = OK, 
                    Message = $"Collection with {collection.Count()} article resources found",
                    DTO = collection
                };
        }

        public async  Task<Result> GetAllVideoResourcesAsync()
        {
            var collection = await _resourceRepo.GetAllVideosAsync();
            return new Result
                {
                    Response = OK, 
                    Message = $"Collection with {collection.Count()} video resources found",
                    DTO = collection
                };
        }

        public async Task<Result> GetAllFromOfficialDocumentationAsync()
        {
            var collection = await _resourceRepo.GetAllFromOfficialDocumentaionAsync();
            return new Result
                {
                    Response = OK, 
                    Message = $"Collection with {collection.Count()} resources found from official documentations",
                    DTO = collection
                };
        }

        public async Task<Result> GetAllResourcesWithProvidedTags(ICollection<string> stringTags)
        {
            if (!stringTags.Any())
                return new Result
                    {
                        Response = BadRequest, 
                        Message = "You need to provide a minimum of one tag to search for",
                    };
            
            // TODO : check if all tags are valid via tagRepo

            var collection = await _resourceRepo.GetAllWithTagsAsyc(stringTags);
            return new Result
                {
                    Response = OK, 
                    Message = $"Collection with {collection.Count()} resources found, that contains the provided tags",
                    DTO = collection
                };
        }
    }
}