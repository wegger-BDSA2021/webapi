using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Data;

namespace Repository.Tests
{
    public class TagRepositoryTests : TestDataGenerator
    {
        [Fact]
        public async void Given_seeded_db_returns_tagname_dotnet()
        {
            // initiate the relevant repo for each unit test
            var _repo = new TagRepository(_context);
            Seed(_context);
            
            var tag = await _repo.GetTagByIdAsync(1);

            Assert.Equal("dotnet", tag.Name);
            // optionally seed the in-memory sqlite database with some dummy data
            // see the Seed method in TestDataGenerator
            //      - in the seed method you can put any kind of data you want to test with 

            
            //Given
        
            //When
        
            //Then
        }
    }
}