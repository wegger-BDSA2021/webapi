using System.Net;
using api.src;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace api.tests.Controller.Tests
{

    [Xunit.Collection("Sequential")]
    public class InitialTest : TestFixture
    {
        public InitialTest(WebApplicationFactory<Startup> factory) : base(factory)
        {

        }

        // [Fact]
        // public async void TestNametesttesttest()
        // {
        //     var response = await Client.GetAsync("/api/Resource/ReadAll");
        //     Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        // }

        [Fact]
        public async void Test_auth()
        {
            // Client.SetFakeBearerToken((object)token);
            var response = await Client.GetAsync("/api/Resource/ReadAll");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}