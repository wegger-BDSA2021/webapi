using System;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using api.src;
using Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Repository.Tests;
using WebMotions.Fake.Authentication.JwtBearer;
using Xunit;

namespace api.tests.Controller.Tests
{
    public class TestFixture : IClassFixture<WebApplicationFactory<Startup>>
    {
        protected readonly WebApplicationFactory<Startup> Factory;
        protected HttpClient Client;
        private string _connectionString = "DataSource=:memory";
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
                });
                

            })
            .CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

    }
}