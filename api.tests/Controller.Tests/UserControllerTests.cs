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
    public class UserControllerTests : TestFixture
    {
        public UserControllerTests(WebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async void Post_returns_HttpStatusCode_Created()
        {
            //Arrange
            var user = new User
            { 
                Id = "testId",
                Resources = null,
                Comments = null,
                Ratings = null
            };

            JsonContent content = JsonContent.Create(user);
            
            //Act
            var response = await Client.PostAsync("/api/Comment", content);

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async void Delete_returns_HttpStatusCode_OK()
        {
            var response = await Client.DeleteAsync("/api/Comment{1}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}