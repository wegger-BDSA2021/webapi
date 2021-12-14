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
            
            Assert.Equal(NotFound, actual.Response);       
        }
         [Fact]
        public async void Given_An_Entry_Returns_An_Ok()
        {
            var _repo = new TagRepository(_context);
            Seed(_context);

            var actual = await _repo.GetTagByIdAsync(1);

            var response = actual.Response;
            var Tag = actual.TagDetailsDTO;

            Assert.Equal(OK, response);
            Assert.Equal(1,Tag.Id);
            Assert.Equal("dotnet",Tag.Name );
        }

        [Fact]
        public async void Given_empty_db_readAllAsync_returns_readonlylist_of_length_0()
        {
            var _repo = new TagRepository(_context);

            var empty = await _repo.GetAllTagsAsync();
            Assert.Empty(empty);
        }
        
    }
}