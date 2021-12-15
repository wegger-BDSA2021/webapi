using api.src.Controllers;
using api.src.Services;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Tests;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Data.Response;
using Moq;


namespace api.tests.Service.Tests
{
    public class RatingServiceTests
    {
        private readonly RatingService _ratingService;
        private readonly Mock<IRatingRepository> _RatingRepoMock = new();

        public RatingServiceTests()
        {
            _ratingService = new RatingService(_RatingRepoMock.Object);
        }
        [Fact]
        public async Task Delete_given_non_existing_returns_NotFound()
        {
            //Arrange
            _RatingRepoMock.Setup(c => c.DeleteAsync(42)).ReturnsAsync(NotFound);

            //Act
            var response = await _ratingService.Delete(42);

            //Assert
            Assert.Equal(NotFound, response.Response);
            Assert.Equal("No comment found with id 42", response.Message);
        }
        [Fact]
        public async Task Given_OOB_rating_UpdateAsync_returns_BadRequest_and_message()
        {
            //Arrange
            var dto = new RatingUpdateDTO 
            {
                Id = 1,
                UpdatedRating = 69, 
            };

            _RatingRepoMock.Setup(c => c.UpdateAsync(dto)).ReturnsAsync(BadRequest);

            //Act
            var result = await _ratingService.UpdateAsync(dto);

            //Assert
            Assert.Equal(BadRequest, result.Response);
            Assert.Equal("Invalid rating",result.Message);
        }
        [Fact]
        public async Task Given_negative_ratingId_UpdateAsync_returns_BadRequest_and_message()
        {
            //Arrange
            var dto = new RatingUpdateDTO 
            {
                Id = -69,
                UpdatedRating = 5, 
            };

            _RatingRepoMock.Setup(c => c.UpdateAsync(dto)).ReturnsAsync(BadRequest);

            //Act
            var result = await _ratingService.UpdateAsync(dto);

            //Assert
            Assert.Equal(BadRequest, result.Response);
            Assert.Equal("Id can only be a positive integer",result.Message);
        }
        
        [Fact]
        public async Task Given_non_existing_ratingId_UpdateAsync_returns_NotFound_and_message()
        {
            //Arrange
            var dto = new RatingUpdateDTO 
            {
                Id = 1,
                UpdatedRating = 5, 
            };

            _RatingRepoMock.Setup(c => c.UpdateAsync(dto)).ReturnsAsync(NotFound);

            //Act
            var result = await _ratingService.UpdateAsync(dto);

            //Assert
            Assert.Equal(NotFound, result.Response);
            Assert.Equal("No Rating found with the given entity",result.Message);
        }
        
        [Fact]
        public async Task Given_valid_ratingId_UpdateAsync_returns_Updated_and_message()
        {
            //Arrange
            int ratingId = 1;
            
            var dto = new RatingUpdateDTO 
            {
                Id = 1,
                UpdatedRating = 5, 
            };

            _RatingRepoMock.Setup(c => c.UpdateAsync(dto)).ReturnsAsync(Updated);

            //Act
            var result = await _ratingService.UpdateAsync(dto);

            //Assert
            Assert.Equal(Updated, result.Response);
            Assert.Equal($"Rating at index {ratingId} has been updated form having the rating {dto.UpdatedRating} to have {dto.UpdatedRating}",result.Message);
        }

        [Fact]
        public async Task Delete_given_existing_returns_Deleted()
        {
            //Arrange
            _RatingRepoMock.Setup(c => c.DeleteAsync(1)).ReturnsAsync(Deleted);
            
            //Act
            var response = await _ratingService.Delete(1);
            
            //Assert
            Assert.Equal(Deleted,response.Response);
            Assert.Equal("Rating found at index 1",response.Message);
        }
        [Fact]
        public async Task Delete_given_nonexisting_returns_NotFound()
        {
            //Arrange
            _RatingRepoMock.Setup(c => c.DeleteAsync(1)).ReturnsAsync(NotFound);
            
            //Act
            var response = await _ratingService.Delete(1);
            
            //Assert
            Assert.Equal(NotFound,response.Response);
            Assert.Equal("No rating found with the given entity",response.Message);
        }

        [Fact]
        public async Task ReadAsync_given_negative_id_returns_BadRequest()
        {
            //Arrange
            int ratingId = -69;
            
            //Act
            var result = await _ratingService.ReadAsync(ratingId);

            //Assert
            Assert.Equal(BadRequest, result.Response);
            Assert.Equal("Id can only be a positive integer",result.Message);
        }
        [Fact]
        public async Task ReadAsync_given_non_existing_id_returns_NotFound()
        {
            //Arrange
            int ratingId = 69;

            _RatingRepoMock.Setup(c => c.ReadAsync(ratingId)).ReturnsAsync((NotFound,null));

            //Act
            var result = await _ratingService.ReadAsync(ratingId);

            //Assert
            Assert.Equal(NotFound, result.Response);
            Assert.Equal("No tag found with the given entity",result.Message);
        }
        
        [Fact]
        public async Task ReadAsync_given_existing_id_returns_OK()
        {
            //Arrange
            int ratingId = 1;
            IReadOnlyCollection<string> collection = null;

            var DDTO = new RatingDetailsDTO
            (
                1,
                "testUserId",
                2,
                1
            );

            _RatingRepoMock.Setup(c => c.ReadAsync(ratingId)).ReturnsAsync((OK,DDTO));

            //Act
            var result = await _ratingService.ReadAsync(ratingId);

            //Assert
            Assert.Equal(OK, result.Response);
            Assert.Equal($"Tag found at index {ratingId}",result.Message);
            Assert.NotNull(result.DTO);
        }

        [Fact]
        public async Task CreateAsync_given_null_rating_returns_BadRequest()
        {
            //Arrange
            
            //Act
            var result = await _ratingService.CreateAsync(null);
            
            //Assert
            Assert.Equal(BadRequest,result.Response);
            Assert.Equal("No tag given",result.Message);
        }
        [Fact]
        public async Task CreateAsync_Given_invalid_new_rating_returns_badrequest_and_null()
        {
            //Arrange
            IReadOnlyCollection<string> collection = null;
            var newDTO = new RatingCreateDTO
            {
                UserId = "testUserId",
                ResourceId = 2,
                Rated = 69
            };
            _RatingRepoMock.Setup(c => c.CreateAsync(newDTO)).ReturnsAsync((BadRequest,null));
            
            //Act
            var result = await _ratingService.CreateAsync(newDTO);
            //Assert
            Assert.Equal(BadRequest,result.Response);
            Assert.Equal( "Invalid rating", result.Message);
            Assert.Null(result.DTO);
        }
        [Fact]
        public async Task CreateAsync_Given_valid_new_rating_returns_Created_and_DTO()
        {
            //Arrange
            IReadOnlyCollection<string> collection = null;
            var newDTO = new RatingCreateDTO
            {
                UserId = "testUserId",
                ResourceId = 2,
                Rated = 1
            };
            var DDTO = new RatingDetailsDTO
            (
                1,
                "testUserId",
                2,
                1
            );
            _RatingRepoMock.Setup(c => c.CreateAsync(newDTO)).ReturnsAsync((Created,DDTO));
            
            //Act
            var result = await _ratingService.CreateAsync(newDTO);
            //Assert
            Assert.Equal(Created,result.Response);
            Assert.Equal( "A new rating was succesfully created", result.Message);
            Assert.NotNull(result.DTO);
        }
        public async Task ReadAsync_given_negative_resid_returns_BadRequest()
        {
            //Arrange
            string userId = "testuserid";
            int resId = -69;
            
            //Act
            var result = await _ratingService.ReadAsync(userId,resId);

            //Assert
            Assert.Equal(BadRequest, result.Response);
            Assert.Equal("Id can only be a positive integer",result.Message);
        }
        [Fact]
        public async Task ReadAsync_given_non_existing_ids_returns_NotFound()
        {
            //Arrange
            string userId = "testuserid";
            int resId = 69;

            _RatingRepoMock.Setup(c => c.ReadAsync(userId,resId)).ReturnsAsync((NotFound,null));

            //Act
            var result = await _ratingService.ReadAsync(userId,resId);

            //Assert
            Assert.Equal(NotFound, result.Response);
            Assert.Equal("No tag found with the given entity",result.Message);
        }
        
        [Fact]
        public async Task ReadAsync_given_existing_ids_returns_OK()
        {
            //Arrange
            string userId = "testuserid";
            int resId = 1;
            var DDTO = new RatingDetailsDTO
            (
                1,
                "testUserId",
                2,
                1
            );

            _RatingRepoMock.Setup(c => c.ReadAsync(userId,resId)).ReturnsAsync((OK,DDTO));

            //Act
            var result = await _ratingService.ReadAsync(userId,resId);

            //Assert
            Assert.Equal(OK, result.Response);
            Assert.Equal($"Tag found at index {1}",result.Message);
            Assert.NotNull(result.DTO);
        }
        [Fact]
        public async Task GetAllRatingFormResourceAsync_given_negative_id_returns_BadRequest()
        {
            //Arrange
            int ratingId = -69;
            
            //Act
            var result = await _ratingService.ReadAllRatingFormRepositoryAsync(ratingId);

            //Assert
            Assert.Equal(BadRequest, result.Response);
            Assert.Equal("Id can only be a positive integer",result.Message);
        }
        //TODO last test of succesfull return of ReadAllRatingFormRepositoryAsync
        /* 
        [Fact]
        public async Task GetAllRatingFormResourceAsync_given_negative_id_returns_Ok_and_DTOS()
        {
            //Arrange
            int ratingId = 1;
            
            //Act
            var result = await _ratingService.ReadAllRatingFormRepositoryAsync(ratingId);

            //Assert
            Assert.Equal(BadRequest, result.Response);
            Assert.Equal("Id can only be a positive integer",result.Message);
        }*/

    }
}