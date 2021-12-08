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
        public async void Given_An_Entry_Returns_An_Ok()
        {
            var _repo = new TagRepository(_context);
            Seed(_context);

            var actual = await _repo.GetTagByIdAsync(1);

            var response = actual.Response;
            var Tag = actual.Tag;

            Assert.Equal(response, OK);
            Assert.Equal(Tag.Id, 1);
            Assert.Equal(Tag.Name, "dotnet");
        }

        [Fact]
        public async void Given_empty_db_readAllAsync_returns_readonlylist_of_length_0()
        {
            var _repo = new TagRepository(_context);

            var empty = await _repo.GetAllTagsAsync();
            Assert.Equal(empty.Count(), 0);
        
        }
        // [Fact]
        // public async void Given_Resource_returns_tags()
        // {
        //     var _repo = new TagRepository(_context);
        //     Seed(_context);

        //     var actual = await _repo.GetAllTagsFormRepositoryAsync(_context.Resources.Find(1));

        //     Assert.Equal(actual.Count, 1);
        //     Assert.Equal(actual.First().Id, 1);
        //     Assert.Equal(actual.First().Name, "dotnet");
        //     Assert.Equal(actual.First().Resources.Count, 1);
        //     Assert.Equal(actual.First().Resources.First(), _context.Resources.Find(1));
        // }

        // [Fact]
        // public async void Given_something_returns_something()
        // {
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
        // }
    }
}