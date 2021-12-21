using Xunit;
using System.Linq;
using Data;
using static Data.Response;

namespace Repository.Tests
{
    public class RatingRepositoryTests : TestDataGenerator
    {
        //TODO create test for CreateAsync and ReadAsync(string,int)
        [Fact]
        public async void Given_ratingcreateDTO_returns_Created_and_RatingDetailsDTO()
        {
            var _repo = new RatingRepository(_context);
            Seed(_context);

            var newDTO = new RatingCreateDTO
            {
                UserId = "testUserId",
                ResourceId = 2,
                Rated = 2,
            };

            var result = await _repo.CreateAsync(newDTO);

            var expected = new RatingDetailsDTO(
                4, 
                newDTO.UserId, 
                newDTO.ResourceId,
                newDTO.Rated
                );

            Assert.Equal(Created,result.Response);
            Assert.Equal(expected,result.RatingDetailsDTO);

        }

        [Theory]
        [InlineData(-1)]
        [InlineData(6)]
        public async void Given_Rating_bigger_outside_valid_range_returns_Conflict_and_null(int Rating)
        {
            var _repo = new RatingRepository(_context);
            Seed(_context);
            
            var newDTO = new RatingCreateDTO
            {
                UserId = "testUserId",
                ResourceId = 2,
                Rated = Rating,
            };

            var result = await _repo.CreateAsync(newDTO);
            
            Assert.Equal(Conflict,result.Response);
            Assert.Null(result.RatingDetailsDTO);
        }


        [Fact]
        public async void Given_no_entries_to_read_returns_NotFound()
        {
            var _repo = new RatingRepository(_context);

            var actual = await _repo.ReadAsync(1);
            
            Assert.Equal(NotFound, actual.Response);       
        }
        
        [Fact] //TODO something is weird here SonarCloud doesnt see the test as valid?
        public async void Given_recource_with_two_readAllAsync_returns_readonlylist_of_length_2()
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

            Assert.Equal(3, rating.RatingDetailsDTO.Rated);
            Assert.Equal(1, rating.RatingDetailsDTO.Id);
            Assert.Equal(1, rating.RatingDetailsDTO.ResourceId);
            Assert.Equal("testUserId", rating.RatingDetailsDTO.UserId);
        }

        [Fact]
        public async void Given_valid_rating_to_update_returns_new_Ratings_and_update()
        {
            var _repo = new RatingRepository(_context);
            Seed(_context);
                        
            var rating = await _repo.ReadAsync(1);
            Assert.Equal(3, rating.RatingDetailsDTO.Rated);

            var dto = new RatingUpdateDTO 
            {
                Id = 1,
                UpdatedRating = 5, 
            };

            var result = await _repo.UpdateAsync(dto);

            Assert.Equal(Updated, result);

            var updated = await _repo.ReadAsync(1);
            Assert.Equal(5, updated.RatingDetailsDTO.Rated);
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


        [Fact]
        public async void Given_Valid_userid_and_resourceid_returns_OK_and_UserRating_on_resource()
        {
            var _repo = new RatingRepository(_context);
            Seed(_context);

            var expectedRating = await _repo.ReadAsync("testUserId", 1);
            
            Assert.Equal(OK,expectedRating.Response);
            Assert.Equal("testUserId",expectedRating.RatingDetailsDTO.UserId);
            Assert.Equal(1,expectedRating.RatingDetailsDTO.ResourceId);
        }
        
        [Fact ]
        public async void Given_User_with_no_ratings_on_resource_returns_NotFound_and_null()
        {
            var _repo = new RatingRepository(_context);
            Seed(_context);

            var expectedRating = await _repo.ReadAsync("secondUserId", 1);
            
            Assert.Equal(NotFound,expectedRating.Response);
            Assert.Null(expectedRating.RatingDetailsDTO);
        }

    }
}