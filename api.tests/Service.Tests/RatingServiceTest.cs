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
    }
}