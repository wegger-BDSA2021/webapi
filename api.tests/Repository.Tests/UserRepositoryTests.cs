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
        public async void Given_valid_user_id_returns_user()
        {
            var _userRepository = new UserRepository(_context);
            Seed(_context);
            var actual = await _userRepository.GetUserByIdAsync(1);

            Assert.Equal(1, actual.Id);
        }

        [Fact]
        public async void Given_non_valid_user_id_returns_null()
        {
            var _userRepository = new UserRepository(_context);
            var actual = await _userRepository.GetUserByIdAsync(1);
            
            Assert.Equal(null , actual); 
            

        }

        [Fact]
        public async void Given_empty_db_readAllAsync_returns_readonlylist_of_length_0()
        {
            var _userRepository = new UserRepository(_context);

            var empty = await _userRepository.GetAllUsersAsync();
            Assert.Equal(0, empty.Count);
        
        }


        [Fact]
        public async void Given_populated_db_readAllAsync_returns_readonlylist_of_length_2()
        {
            var _userRepository = new UserRepository(_context);
            Seed(_context);
            
            var allUsers = await _userRepository.GetAllUsersAsync();
            Assert.Equal(2,allUsers.Count);
            
        }
        
    }
}