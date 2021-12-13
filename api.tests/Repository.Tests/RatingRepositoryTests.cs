using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Data;
using static Data.Response;

namespace Repository.Tests
{
    public class RatingRepositoryTests : TestDataGenerator
    {
        [Fact]
        public async void Given_no_entries_returns_NotFound()
        {
            var _repo = new RatingRepository(_context);

            var actual = await _repo.ReadAsync(1);
            
            Assert.Equal(actual.Response, NotFound);       
        }
        public async void Given_recource_with_two_readAllAsync_returns_readonlylist_of_length_0()
        {
            var _repo = new RatingRepository(_context);
            Seed(_context);
            var tworatings = await _repo.GetAllRatingFormResourceAsync(1);
            Assert.Equal(tworatings.Count(), 2);
        }
        [Fact]
        public async void Given_something_returns_something()
        {
            // initiate the relevant repo for each unit test
            var _repo = new RatingRepository(_context);
            Seed(_context);
            
            var rating = await _repo.ReadAsync(1);

            Assert.Equal(3, rating.Rating.Rated);
            Assert.Equal(1, rating.Rating.Id);
            Assert.Equal(1, rating.Rating.ResourceId);
            Assert.Equal("testUserId", rating.Rating.UserId);
        }

        [Fact]
        public async void Given_update_returns_new_Ratings()
        {
            var _repo = new RatingRepository(_context);
            Seed(_context);
                        
            var rating = await _repo.ReadAsync(1);
            Assert.Equal(3, rating.Rating.Rated);

            var dto = new RatingUpdateDTO 
            {
                Id = 1,
                UpdatedRating = 5, 
            };

            var result = await _repo.UpdateAsync(dto);

            Assert.Equal(Updated, result);

            var updated = await _repo.ReadAsync(1);
            Assert.Equal(5, updated.Rating.Rated);
        }
        
        [Fact]
        public async void Given_Valid_Rating_Id_Returns_DeletedResponse()
        {
            var _ratingrepo = new RatingRepository(_context);
            Seed(_context);
            
            var rating = await _ratingrepo.DeleteAsync(1);
            
            
            Assert.Equal(Deleted,rating);
        }
        [Fact] 
        public async void Given_invalid_Rating_Id_Returns_NotFound()
        {
            var _ratingrepo = new RatingRepository(_context);
            
            var invalidRating = await _ratingrepo.DeleteAsync(1);
            
            Assert.Equal(NotFound, invalidRating);
        }

        [Fact]
        public async void Given_DTO_Rating_between_0_and_6_returns_Updated()
        {
            throw new NotImplementedException();
        }
        
        [Fact]
        public async void Given_DTO_Rating_smaller_than_1_and_bigger_than_5_returns_Conflict()
        {
            throw new NotImplementedException();
        }
        
        [Fact]
        public async void Given_Invalid_DTO_Rating_returns_NotFound()
        {
            throw new NotImplementedException();
        }

    }
}