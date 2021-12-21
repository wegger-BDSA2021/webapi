using System.Net;
using api.src;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace api.tests.Controller.Tests
{
    [Xunit.Collection("Sequential")]
    public class ResourceControllerTests : TestFixture
    {
        public ResourceControllerTests(WebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async void GetAllFromOfficialDocumentationAsync_returns_HttpStatusCode_OK()
        {
            var response = await Client.GetAsync("/api/Resource/ReadAllOfficialDocumentation");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetAllVideoResourcesAsync_returns_HttpStatusCode_OK()
        {
            var response = await Client.GetAsync("/api/Resource/ReadAllVideos");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetAllArticleResourcesAsync_returns_HttpStatusCode_OK()
        {
            var response = await Client.GetAsync("/api/Resource/ReadAllArticles");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetAllResourcesMarkedDeprecatedAsync_returns_HttpStatusCode_OK()
        {
            var response = await Client.GetAsync("/api/Resource/ReadAllDeprecated");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void ReadAllWhereTitleContians_returns_HttpStatusCode_OK()
        {
            var matcher = "test";

            var response = await Client.GetAsync($"/api/Resource/ReadAllWithTitle/{matcher}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetAllResourcesWithinRangeAsync_returns_HttpStatusCode_OK()
        {

            var response = await Client.GetAsync("/api/Resource/ReadAllWithAverageRatingInRange/1/2");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetAllResourcesFromDomainAsync_returns_HttpStatusCode_OK()
        {     
            var matcher = "test";

            var response = await Client.GetAsync($"/api/Resource/ReadAllFromDomain/{matcher}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetAllResourcesFromUserAsync_returns_HttpStatusCode_OK()
        {     
            var id = "testUserId";

            var response = await Client.GetAsync($"/api/Resource/ReadAllFromUser/{id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetAllResourcesFromUserAsync_returns_HttpStatusCode_BadRequest()
        {     
            var id = "";

            var response = await Client.GetAsync($"/api/Resource/ReadAllFromUser/{id}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void GetAllResourcesFromUserAsync_returns_HttpStatusCode_NotFound()
        {     
            var id = "IDontExist";

            var response = await Client.GetAsync($"/api/Resource/ReadAllFromUser/{id}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void ReadSingleResource_returns_HttpStatusCode_OK()
        {     
            var response = await Client.GetAsync("/api/Resource/1");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async void GetById_given_negative_returns_HttpStatusCode_BadRequest()
        {
            var Resourceid = -69;

            var response = await Client.GetAsync($"/api/Resource/{Resourceid}");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async void ReadSingleResource_returns_HttpStatusCode_NotFound()
        {     
            var response = await Client.GetAsync("/api/Resource{69}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async void ReadAll_returns_HttpStatusCode_OK()
        {     
            var response = await Client.GetAsync($"/api/Resource/ReadAll");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void Get_returns_HttpStatusCode_OK()
        {
            var response = await Client.GetAsync("/api/Resource/ReadAll");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async void GetById_returns_HttpStatusCode_OK()
        {
            var Resourceid = 1;
            var response = await Client.GetAsync($"/api/Resource{Resourceid}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        
    }
}
