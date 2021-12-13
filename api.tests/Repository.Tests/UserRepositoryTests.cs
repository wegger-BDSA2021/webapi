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
            Assert.Equal(NotFound, actual.Response);
            Assert.Null(actual.User);
        }

        [Fact]
        public async void Given_existing_returns_OK_and_entity()
        {
            var _repo = new UserRepository(_context);
            Seed(_context);

            var actual = await _repo.GetUserByIdAsync("testUserId");
            Assert.Equal(OK, actual.Response);
            Assert.NotNull(actual.User);
            Assert.Equal("testUserId", actual.User.Id);
        }

        [Fact]
        public async void Given_non_existing_Id_returns_created_and_Id_when_creating_new()
        {
            var _repo = new UserRepository(_context);
            Seed(_context);

            var guid = Guid.NewGuid().ToString();
            var actual = await _repo.CreateUserAsync(guid);

            Assert.Equal(Created, actual.Response);
            Assert.Equal(guid, actual.Id);
            //Assert.Equal(2, _context.Users.Count());
        }

        [Fact]
        public async void Given_existing_Id_returns_BadRequest_when_creating_new()
        {
            var _repo = new UserRepository(_context);
            Seed(_context);

            var actual = await _repo.CreateUserAsync("testUserId");

            Assert.Equal(BadRequest, actual.Response);
            Assert.Null(actual.Id);
        }

        [Fact]
        public async void Given_non_existing_returns_notfound_when_deleting_user()
        {
            var _repo = new UserRepository(_context);
            Seed(_context);

            var guid = Guid.NewGuid().ToString();
            var response = await _repo.DeleteUserAsync(guid);

            Assert.Equal(NotFound, response);
            //Assert.Equal(1, _context.Users.Count());
        }

        [Fact]
        public async void Given_existing_returns_deleted_and_cascade_deletes_relationships_when_deleting_user()
        {
            var _repo = new UserRepository(_context);
            Seed(_context);

            var response = await _repo.DeleteUserAsync("testUserId");

            Assert.Equal(Deleted, response);
            //Assert.False(_context.Users.Any());
            Assert.False(_context.Ratings.Any());
            Assert.False(_context.Comments.Any());
        }

        [Fact]
        public async void Given_existing_returns_true()
        {
            var _repo = new UserRepository(_context);
            Seed(_context);

            var actual = await _repo.UserExists("testUserId");
            Assert.True(actual);
        }

        [Fact]
        public async void Given_non_existing_returns_false()
        {
            var _repo = new UserRepository(_context);
            Seed(_context);
            
            var guid = Guid.NewGuid().ToString();
            var actual = await _repo.UserExists(guid);

            Assert.False(actual);
        }
        
    }
}