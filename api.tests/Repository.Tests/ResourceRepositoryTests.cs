using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Data;
using static Data.Response;

namespace Repository.Tests
{
    public class ResourceRepositoryTests : TestDataGenerator
    {
        [Fact]
        public async void Given_no_entries_returns_NotFound()
        {
            var _repo = new ResourceRepository(_context);

            var actual = await _repo.ReadAsync(1);
            Assert.Equal(NotFound, actual.Response);
        }

        [Fact]
        public async void Given_one_entry_returns_OK_and_the_first_resource()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var actual = await _repo.ReadAsync(1);

            var response = actual.Response;
            var resource = actual.ResourceDetails;

            Assert.Equal(OK, response);
            Assert.Equal("resource_1", resource.Title);
            Assert.Equal("dotnet", resource.Tags.First());
            Assert.Equal("test", resource.Description);
            Assert.Equal(false, resource.Deprecated);
            Assert.Equal("https://github.com/wegger-BDSA2021/webapi/tree/develop", resource.Url);
            Assert.Equal(_dateForFirstResource, resource.TimeOfReference);
            Assert.Equal(_dateForFirstResource, resource.LastCheckedForDeprecation);
            Assert.NotNull(resource.Comments.FirstOrDefault());
            Assert.NotNull(resource.Ratings.FirstOrDefault());
            Assert.Equal(3, resource.Ratings.FirstOrDefault());
            Assert.Equal(4.0, resource.AverageRating);
        }

        [Fact]
        public async void Given_seededDB_readAllAsync_returns_readonlylist_of_length_1()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var allResources = await _repo.ReadAllAsync();
            Assert.Equal(2, allResources.Count());
            Assert.Equal("resource_1", allResources.First().Title);
        }

        [Fact]
        public async void Given_empty_db_readAllAsync_returns_readonlylist_of_length_0()
        {
            var _repo = new ResourceRepository(_context);

            var empty = await _repo.ReadAllAsync();
            Assert.Equal(0, empty.Count());
        }

        [Fact]
        public async void Given_dotnet_stringTag_returns_list_with_all_entries_having_dotnet_tag()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var tagsList = new[] { "dotnet" };

            var resourcesWithDotnet = await _repo.GetAllWithTagsAsyc(tagsList);
            Assert.Equal(1, resourcesWithDotnet.Count());

            var acutalResource = resourcesWithDotnet.FirstOrDefault();
            Assert.NotNull(acutalResource);
            Assert.Equal("resource_1", acutalResource.Title);
        }

        [Fact]
        public async void Given_nonExisting_Tag_returns_no_entries()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var tagsList = new[] { "dummy", "dotnet", "linq" };

            var resources = await _repo.GetAllWithTagsAsyc(tagsList);
            Assert.Equal(0, resources.Count());
        }

        [Fact]
        public async void Given_new_resourceDTO_with_existing_URL_returns_Conflict()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var newResource = new ResourceCreateDTOServer
            {
                TitleFromUser = "this is a new resource",
                UserId = 1,
                Description = "description",
                TimeOfReference = _dateForFirstResource,
                Url = "https://github.com/wegger-BDSA2021/webapi/tree/develop",
                InitialRating = 4,
                Deprecated = false,
                LastCheckedForDeprecation = _dateForFirstResource
            };

            var actual = await _repo.CreateAsync(newResource);
            var expected = Conflict;

            Assert.Equal(expected, actual.Response);
        }

        [Fact]
        public async void Given_new_resourceDTO_returns_Created_and_correct_DTO()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var newResource = new ResourceCreateDTOServer
            {
                TitleFromUser = "this is a new resource",
                UserId = 1,
                Description = "description",
                TimeOfReference = _dateForFirstResource,
                Url = "https://github.com/wegger-BDSA2021/webapi/tree/develop/blabla",
                InitialRating = 4,
                Deprecated = false,
                LastCheckedForDeprecation = _dateForFirstResource, 
                HostBaseUrl = "www.github.com",
                IsVideo = false, 
                IsOfficialDocumentation = false, 
                TitleFromSource = "Some fancy title"
            };

            var result = await _repo.CreateAsync(newResource);
            var response = result.Response;
            var createdDTO = result.CreatedResource;

            Assert.Equal(Created, response);

            Assert.Equal(3, createdDTO.Id);
            Assert.Equal("this is a new resource", createdDTO.Title);
            Assert.Equal("description", createdDTO.Description);
            Assert.Equal(_dateForFirstResource, createdDTO.TimeOfReference);
            Assert.Equal("https://github.com/wegger-BDSA2021/webapi/tree/develop/blabla", createdDTO.Url);
            Assert.Equal(0, createdDTO.Tags.Count());
            Assert.Equal(1, createdDTO.Ratings.Count());
            Assert.Equal(4, createdDTO.AverageRating);
            Assert.Equal(0, createdDTO.Comments.Count());
            Assert.Equal(false, createdDTO.Deprecated);
            Assert.Equal(_dateForFirstResource, createdDTO.LastCheckedForDeprecation);
        }

        [Fact]
        public async void Given_new_rating_for_resource_returns_correct_average()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var rating = new Rating{
                UserId = 1,
                ResourceId = 1, 
                Rated = 1, 
            };

            var oldAverage = await _repo.GetAverageRatingByIdAsync(1);
            Assert.Equal(4, oldAverage.Average);

            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();

            var newAverage = await _repo.GetAverageRatingByIdAsync(1);
            Assert.Equal(3, newAverage.Average);
        }

        [Fact]
        public async void Given_range_returns_correct_entries_and_computed_average()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var allWithRatingInRange = await _repo.GetAllWithRatingInRangeAsync(4, 5);
            Assert.NotEmpty(allWithRatingInRange);
            Assert.Equal(2, allWithRatingInRange.Count()); 
            Assert.Equal(4, allWithRatingInRange.FirstOrDefault().AverageRating); 

            var rating = new Rating{
                UserId = 1,
                ResourceId = 1, 
                Rated = 1, 
            };
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();

            var newQuery = await _repo.GetAllWithRatingInRangeAsync(5, 5);
            Assert.NotEmpty(newQuery);
            Assert.Equal(5, newQuery.FirstOrDefault().AverageRating); 
            Assert.Equal("resource_2", newQuery.FirstOrDefault().Title); 
        }

        [Fact]
        public async void Given_non_existing_userId_returns_empty_list_of_resources()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var result = await _repo.GetAllFromUserAsync(2);
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async void Given_existing_userId_returns_nonempty_list_of_resources()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var result = await _repo.GetAllFromUserAsync(1);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("resource_1", result.FirstOrDefault().Title);
        }

        [Fact]
        public async void Given_res_returns_all_resources_with_res_in_title()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var result = await _repo.GetAllWhereTitleContainsAsync("res");

            Assert.NotEmpty(result);
            Assert.Equal("resource_1", result.FirstOrDefault().Title);
        }

        [Fact]
        public async void Given_blob_returns_no_resources()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var result = await _repo.GetAllWhereTitleContainsAsync("blob");

            Assert.Empty(result);
            Assert.Null(result.FirstOrDefault());
        }
    }
}