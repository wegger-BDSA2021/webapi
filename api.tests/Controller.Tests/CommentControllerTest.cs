using api.src;
using Data;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace api.tests.Controller.Tests
{
    [Xunit.Collection("Sequential")]
    public class CommentControllerTest : TestFixture
    {
        public CommentControllerTest(WebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async void Get_returns_HttpStatusCode_OK()
        {
            var response = await Client.GetAsync("/api/Comment");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetById_returns_HttpStatusCode_OK()
        {
            var response = await Client.GetAsync("/api/Comment1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Post_returns_HttpStatusCode_Created()
        {
            //Arrange
            var comment = new CommentCreateDTOServer
            {
                UserId = "testUserId",
                ResourceId = 1,
                TimeOfComment = DateTime.Now,
                Content = "This is a new comment",
            };

            JsonContent content = JsonContent.Create(comment);

            //Act
            var response = await Client.PostAsync("/api/Comment", content);

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async void Update_returns_HttpStatusCode_OK()
        {
            //Arrange
            var comment = new CommentUpdateDTO
            {
                Id = 1,
                UserId = "testUserId",
                ResourceId = 1,
                TimeOfComment = DateTime.Now,
                Content = "This is a updated comment",
            };

            JsonContent content = JsonContent.Create(comment);

            //Act
            var response = await Client.PutAsync("/api/Comment", content);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Delete_returns_HttpStatusCode_OK()
        {
            var response = await Client.DeleteAsync("/api/Comment1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
