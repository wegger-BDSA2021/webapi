// using System.Net;
// using System.Net.Http;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Xunit;

// namespace api.tests.Controller.Tests
// {
//     public class TestSetup : IClassFixture<Factory>
//     {
//         private readonly HttpClient _client;
//         private readonly Factory _factory;

//         public TestSetup(Factory factory)
//         {
//             _factory = factory;
//             _client = factory.CreateClient(new WebApplicationFactoryClientOptions
//             {
//                 AllowAutoRedirect = false
//             });
//         }

//         [Fact]
//         public async Task initial_test_come_on()
//         {
//             var resources = await _client.GetAsync("/api/Resource/ReadAll");
//             Assert.Equal(HttpStatusCode.OK, resources.StatusCode);
//         }

//     }
// }