using api.src;
using api.src.Controllers;
using api.src.Services;
using Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Data.Response;

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

        /*[Fact]
        public async void GetById_returns_HttpStatusCode_OK()
        {
            var response = await Client.GetAsync("/api/Comment{1}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }*/

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
            //var response = Client.PostAsync("/api/Comment", json).Result;

            HttpResponseMessage response = await Client.PostAsync("/api/Comment", content);

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        /*
        [Fact]
        public async void Update_returns_HttpStatusCode_OK()
        {
            var response = await Client.PutAsync("/api/Comment");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Delete_returns_HttpStatusCode_OK()
        {
            var response = await Client.DeleteAsync("/api/Comment");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }*/
    }
}
