using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Data;
using static Data.Response;

namespace Repository.Tests
{
    public class UserRepositoryTests : TestDataGenerator
    {
        [Fact]
        public async void Given_non_existing_returns_notfound()
        {
            var _repo = new UserRepository(_context);
            Seed(_context);

            var actual = await _repo.GetUserByIdAsync("dummy");
            Assert.Equal(NotFound, actual.Item1);
        }

        [Fact]
        public async void Given_existing_returns_OK_and_entity()
        {
            
        }

        [Fact]
        public async void Given_non_existing_Id_returns_created_and_Id_when_creating_new()
        {
            
        }

        [Fact]
        public async void Given_existing_Id_returns_BadRequest_when_creating_new()
        {
            
        }

        [Fact]
        public async void Given_non_existing_returns_notfound_when_deleting_user()
        {
            
        }

        [Fact]
        public async void Given_existing_returns_deleted_and_cascade_deletes_relationships_when_deleting_user()
        {
            
        }

        [Fact]
        public async void Given_existing_returns_true()
        {
            
        }

        [Fact]
        public async void Given_non_existing_returns_false()
        {
            
        }
    }
}