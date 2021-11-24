using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Data;
using static Data.Response;

namespace Repository.Tests
{
    public class TagRepositoryTests : TestDataGenerator
    {
        [Fact]
        public async void Given_no_entries_returns_NotFound()
        {
            var _repo = new TagRepository(_context);

            var actual = await _repo.GetTagByIdAsync(1);
            
            Assert.Equal(actual.Response, NotFound);       
        }
         [Fact]
        public async void Given_something_else_returns_something_else()
        {

        }
        [Fact]
        public async void Given_something_returns_something()
        {
            // initiate the relevant repo for each unit test
            //var _repo = new TagRepository(_context);
            //Seed(_context);
            
            //var tag = await _repo.GetTagByIdAsync(1);

            //Assert.Equal("dotnet", tag.);
            // optionally seed the in-memory sqlite database with some dummy data
            // see the Seed method in TestDataGenerator
            //      - in the seed method you can put any kind of data you want to test with 

            
            //Given
        
            //When
        
            //Then
        }
    }
}