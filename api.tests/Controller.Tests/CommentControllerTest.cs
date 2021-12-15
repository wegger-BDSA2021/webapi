using api.src;
using api.src.Controllers;
using api.src.Services;
using Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var response = await Client.GetAsync("/api/Comment/ReadAll");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

    }
}
