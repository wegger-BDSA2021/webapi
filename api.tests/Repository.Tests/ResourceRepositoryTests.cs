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
    }
}