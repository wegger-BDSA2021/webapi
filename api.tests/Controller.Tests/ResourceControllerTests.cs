using System.Collections.Generic;
using System.Net;
using api.src.Controllers;
using Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Services;
using Xunit;

namespace api.tests.Controller.Tests
{
    public class ResourceControllerTests : ControllerTestsBase
    {
        private readonly Mock<IResourceService> _mockService;
        private readonly ResourceController _controller;

        public ResourceControllerTests(WebApiTesterFactory factory) : base(factory)
        {
            _mockService = new Mock<IResourceService>();
            _controller = new ResourceController(_mockService.Object);
        }

        [Fact]
        public async void test_of_setup()
        {
            // var result = await _controller.ReadAllResources();
            // Assert.IsType<ICollection<ResourceDTO>>(result);
            client.SetFakeBearerToken((object)token);

            var response = await client.GetAsync("/api/Resource/ReadAll");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<IEnumerable<ResourceDTO>>(json);
            Assert.NotNull(data);
            // data.First().Term.Should().Be("AccessToken");

        }
    }
    
}