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
        protected dynamic token;

        public TestFixture(WebApplicationFactory<Startup> factory)
        {
            token = new ExpandoObject();
            token.sub = Guid.NewGuid();
            token.scope = new[] { "ReadAccess" };

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
                /* setup whatever services you need to override, 
                its useful for overriding db context if you're using Entity Framework */
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

                    var sp = services.BuildServiceProvider();
                    using var scope = sp.CreateScope();
                    using var appContext = scope.ServiceProvider.GetRequiredService<WeggerContext>();
                    appContext.Database.OpenConnection();
                    appContext.Database.EnsureCreated();

                    services.AddMvc(options =>
                    {
                        var policy = new AuthorizationPolicyBuilder()
                            .RequireAuthenticatedUser()
                            .AddAuthenticationSchemes("ReadAccess")
                            .Build();

                        options.Filters.Add(new AuthorizeFilter(policy));
                    });

                    services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = "ReadAccess";
                        options.DefaultChallengeScheme = "ReadAccess";
                        options.DefaultScheme = "ReadAccess";
                    })
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("ReadAccess", options => { });


                });

            })
            .CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

    }
}