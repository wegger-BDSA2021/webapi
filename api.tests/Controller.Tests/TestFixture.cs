using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using api.src;
using Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace api.tests.Controller.Tests
{
    public class TestFixture : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly WebApplicationFactory<Startup> Factory;
        protected HttpClient Client;
        private string _connectionString = "Filename=:memory:";
        private SqliteConnection _connection;

        public TestFixture(WebApplicationFactory<Startup> factory)
        {
            Factory = factory;
            SetupClient();
        }

        protected static HttpContent ConvertToHttpContent<T>(T data)
        {
            var jsonQuery = JsonConvert.SerializeObject(data);
            HttpContent httpContent = new StringContent(jsonQuery, Encoding.UTF8);
            httpContent.Headers.Remove("content-type");
            httpContent.Headers.Add("content-type", "application/json; charset=utf-8");

            return httpContent;
        }

        private void SetupClient()
        {
            _connection = new SqliteConnection(_connectionString);
            _connection.Open();

            Client = Factory.WithWebHostBuilder(builder =>
            {
                
                builder.ConfigureTestServices(services =>
                {
                    
                    var dbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<WeggerContext>));
                    if (dbContext != null)
                    {
                        services.Remove(dbContext);
                    }


                    services.AddDbContext<WeggerContext>(optionsBuilder =>
                    {
                        optionsBuilder.UseSqlite(_connection);
                    });

                    services.AddScoped<IWeggerContext, WeggerContext>();

                    services.AddAuthentication("IntegrationTest")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("IntegrationTest", options => { });

                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    using var appContext = scope.ServiceProvider.GetRequiredService<WeggerContext>();
                    appContext.Database.OpenConnection();
                    appContext.Database.EnsureCreated();

                    Seed(appContext);
                    
                });
                

            })
            .CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        private void Seed(DbContext context)
        {
            var _dateForFirstResource = DateTime.Now;

            var users = new[] {
                new User { Id = "testUserId" },
                new User { Id = "secondUserId" }
            };

            var resources = new[] {
                new Resource { 
                    Id = 1,    
                    UserId = "testUserId",
                    Title = "resource_1", 
                    SourceTitle = "some official title",
                    Description = "test", 
                    TimeOfReference = _dateForFirstResource,
                    Url = "https://github.com/wegger-BDSA2021/webapi/tree/develop", 
                    HostBaseUrl = "www.github.com",
                    LastCheckedForDeprecation = _dateForFirstResource, 
                    IsVideo = false, 
                    IsOfficialDocumentation = false, 
                },
                new Resource { 
                    Id = 2,    
                    UserId = "testUserId",
                    Title = "resource_2", 
                    SourceTitle = "another official title",
                    Description = "test of another", 
                    TimeOfReference = _dateForFirstResource,
                    Url = "https://github.com/wegger-BDSA2021/webapi/tree/develop/test2", 
                    HostBaseUrl = "www.github.com",
                    LastCheckedForDeprecation = _dateForFirstResource,
                    IsVideo = false, 
                    IsOfficialDocumentation = false, 
                }
            };

            var ratings = new [] {
                new Rating {
                    Id = 1,
                    Rated = 3,
                    ResourceId = 1,
                    UserId = "testUserId"
                },
                new Rating {
                    Id = 2,
                    Rated = 5,
                    ResourceId = 1,
                    UserId = "testUserId"
                },
                new Rating {
                    Id = 3,
                    Rated = 5,
                    ResourceId = 2,
                    UserId = "testUserId"
                }
            };

            var comments = new[] {
                new Comment {
                    Id = 1,
                    UserId = "testUserId", 
                    ResourceId = 1,
                    TimeOfComment = DateTime.Now,
                    Content = "Content description"
                }
            };

            var tags = new[] {
                new Tag { Id = 1, Name = "dotnet"},
                new Tag { Id = 2, Name = "linq"},
            };

            resources[0].Tags.Add(tags[0]);

            context.AddRange(users);
            context.AddRange(tags);
            context.AddRange(ratings);
            context.AddRange(resources);
            context.AddRange(comments);

            context.SaveChanges();
        }

    }
}