using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Data;
using SQLitePCL;
using static Data.Response;

namespace Repository.Tests
{
    public class RatingRepositoryTests : TestDataGenerator
    {
        [Fact]
        public async void Given_no_entries_to_read_returns_NotFound()
        {
            var _repo = new RatingRepository(_context);

            var actual = await _repo.ReadAsync(1);
            
            Assert.Equal(NotFound, actual.Response);       
        }
        
        [Fact]
        public async void Given_recource_with_two_readAllAsync_returns_readonlylist_of_length_0()
        {
            var _repo = new RatingRepository(_context);
            Seed(_context);
            var tworatings = await _repo.GetAllRatingFormResourceAsync(1);
            Assert.Equal(2, tworatings.Count());
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
        public async void Given_valid_rating_to_update_returns_new_Ratings_and_update()
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
        public async void Given_non_valid_rating_to_update_bigger_than_5_smaller_than_1_returns_Conflict()
        {
            var _repo = new RatingRepository(_context);
            Seed(_context);
                        
            //var rating = await _repo.ReadAsync(1);
            //Assert.Equal(3, rating.Rating.Rated);

            var dto = new RatingUpdateDTO 
            {
                Id = 1,
                UpdatedRating = 6, 
            };

            var result = await _repo.UpdateAsync(dto);

            Assert.Equal(Conflict, result);
        }

        [Fact]
        public async void Given_non_exisisting_rating_dto_id_to_update_returns_NotFound()
        {
            var _repo = new RatingRepository(_context);

            var dto = new RatingUpdateDTO
            {
                Id = 1,
                UpdatedRating = 3,
            };

            var result = await _repo.UpdateAsync(dto);
            
            Assert.Equal(NotFound,result);
        }

        [Fact]
        public async void Given_Valid_Rating_Id_to_delete_Returns_DeletedResponse()
        {
            var _ratingrepo = new RatingRepository(_context);
            Seed(_context);
            
            var rating = await _ratingrepo.DeleteAsync(1);
            
            
            Assert.Equal(Deleted,rating);
        }
        [Fact] 
        public async void Given_invalid_Rating_Id_to_delete_Returns_NotFound()
        {
            var _ratingrepo = new RatingRepository(_context);

            var invalidRating = await _ratingrepo.DeleteAsync(1);
            
            Assert.Equal(NotFound, invalidRating);
        }

    }
}