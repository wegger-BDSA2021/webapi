using api.src;
using Data;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace api.tests.Controller.Tests
{
    [Xunit.Collection("Sequential")]
    public class RatingControllerTests : TestFixture
    {
        public RatingControllerTests(WebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async void Post_returns_HttpStatusCode_Created()
        {
            //Arrange
            var rating = new RatingCreateDTO
            {
                UserId = "testUserId",
                ResourceId = 1,
                Rated = 2
            };

            JsonContent content = JsonContent.Create(rating);

            //Act
            var response = await Client.PostAsync("/api/Rating", content);

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async void Update_returns_HttpStatusCode_NoContent()
        {
            //Arrange
            var rating = new RatingUpdateDTO
            {
                Id = 1,
                UpdatedRating = 4
            };

            JsonContent content = JsonContent.Create(rating);

            //Act
            var response = await Client.PutAsync("/api/Rating", content);

            //Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async void GetById_returns_HttpStatusCode_OK()
        {
            var response = await Client.GetAsync("/api/Rating/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Delete_returns_HttpStatusCode_NoContent()
        {
            var response = await Client.DeleteAsync("/api/Rating/1");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}
