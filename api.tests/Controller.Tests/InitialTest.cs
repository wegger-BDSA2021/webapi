using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using api.src;
using Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace api.tests.Controller.Tests
{

    [Xunit.Collection("Sequential")]
    public class InitialTest : TestFixture
    {
        public InitialTest(WebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async void Test_auth()
        {
            var response = await Client.GetAsync("/api/Resource/ReadAll");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Test_seeding()
        {
            var response = await Client.GetStringAsync("/api/Resource/ReadAll");
            var collection = JsonSerializer.Deserialize<List<ResourceDTO>>(response);

            Assert.Equal(2, collection.Count());
        }
    }
}