using api.src;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace api.tests.Controller.Tests
{

    [Xunit.Collection("Sequential")]
    public class UserControllerTests : TestFixture
    {
        public UserControllerTests(WebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async void GetById_returns_HttpStatusCode_OK()
        {
            var response = await Client.GetAsync("/api/Comment{1}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        /*[Fact]
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
            var response = await Client.PutAsync("/api/Comment");
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
            var response = await Client.DeleteAsync("/api/Comment");
            var response = await Client.DeleteAsync("/api/Comment{1}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }*/


    }
}
