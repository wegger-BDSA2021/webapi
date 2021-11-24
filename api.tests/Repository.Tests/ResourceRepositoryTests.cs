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
            Assert.Equal(actual.Response, NotFound);       
        }

        [Fact]
        public async void Given_one_entry_returns_OK_and_the_first_resource()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var actual = await _repo.ReadAsync(1);

            var response = actual.Response;
            var resource = actual.Resource;

            Assert.Equal(response, OK);
            Assert.Equal(resource.Title, "resource_1");
            Assert.Equal(resource.Tags.First().Name, "dotnet");
            Assert.Equal(resource.Description, "test");
            Assert.Equal(resource.Deprecated, false);
            Assert.Equal(resource.UserId, 1);
            Assert.Equal(resource.Url, "https://github.com/wegger-BDSA2021/webapi/tree/develop");
            Assert.Equal(resource.TimeOfReference, _dateForFirstResource);
            Assert.Equal(resource.LastCheckedForDeprecation, _dateForFirstResource);
            Assert.Null(resource.Comments.FirstOrDefault());
            Assert.Null(resource.Ratings.FirstOrDefault());
        }

        [Fact]
        public async void Given_seededDB_readAllAsync_returns_readonlylist_of_length_1()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var allResources = await _repo.ReadAllAsync();
            Assert.Equal(allResources.Count(), 1);
        }

        [Fact]
        public async void Given_empty_db_readAllAsync_returns_readonlylist_of_length_0()
        {
            var _repo = new ResourceRepository(_context);

            var empty = await _repo.ReadAllAsync();
            Assert.Equal(empty.Count(), 0);
        }

        [Fact]
        public async void Given_dotnet_tag_returns_list_with_all_entries_having_dotnet_tag()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var tagsList = new []{"dotnet"};

            var resourcesWithDotnet = await _repo.GetAllWithTagsAsyc(tagsList); 
            Assert.Equal(resourcesWithDotnet.Count(), 1);

            var acutalResource = resourcesWithDotnet.FirstOrDefault();
            Assert.NotNull(acutalResource);
            Assert.Equal(acutalResource.Title, "resource_1");
        }

        [Fact]
        public async void Given_nonExisting_Tag_returns_no_entries()
        {
            var _repo = new ResourceRepository(_context);
            Seed(_context);

            var tagsList = new []{"dummy"};

            var resources = await _repo.GetAllWithTagsAsyc(tagsList);
            Assert.Equal(resources.Count(), 0);
        }
    }
}