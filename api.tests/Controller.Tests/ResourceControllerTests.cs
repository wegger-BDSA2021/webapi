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
    public class ResourceControllerTests : TestFixture
    {
        public ResourceControllerTests(WebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async void Get_returns_HttpStatusCode_OK()
        {
            var response = await Client.GetAsync("/api/Resource/ReadAll");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetById_returns_HttpStatusCode_OK()
        {
            var response = await Client.GetAsync("/api/Resource{1}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
