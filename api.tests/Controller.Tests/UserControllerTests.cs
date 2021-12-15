﻿using api.src;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
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
            var response = await Client.PostAsync("/api/User/Create", null);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async void Delete_returns_HttpStatusCode_NoContent()
        {
            var response = await Client.DeleteAsync("/api/User/Delete/secondUserId");

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }
    }
}