// using System;
// using System.Linq;
// using api.src;
// using Data;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc.Authorization;
// using Microsoft.AspNetCore.Mvc.Testing;
// using Microsoft.Data.Sqlite;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
// using Repository.Tests;

// namespace api.tests.Controller.Tests
// {

//     public class Factory : WebApplicationFactory<Program>
//     {
//         protected override IHost CreateHost(IHostBuilder builder)
//         {
//             builder.ConfigureServices(services =>
//             {
//                 var dbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<WeggerTestContext>));

//                 if (dbContext != null)
//                 {
//                     services.Remove(dbContext);
//                 }

//             /* Overriding policies and adding Test Scheme defined in TestAuthHandler */
//                 services.AddMvc(options =>
//                 {
//                     var policy = new AuthorizationPolicyBuilder()
//                         .RequireAuthenticatedUser()
//                         .AddAuthenticationSchemes("Test")
//                         .Build();

//                     options.Filters.Add(new AuthorizeFilter(policy));
//                 });

//                 services.AddAuthentication(options =>
//                 {
//                     options.DefaultAuthenticateScheme = "Test";
//                     options.DefaultChallengeScheme = "Test";
//                     options.DefaultScheme = "Test";
//                 })
//                 .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });

//                 // var connection = new SqliteConnection("Filename=:memory:");
//                 var connection = new SqliteConnection("DataSource=:memory");

//                 services.AddDbContext<WeggerTestContext>(options =>
//                 {
//                     options.UseSqlite(connection);
//                 });

//                 var provider = services.BuildServiceProvider();
//                 using var scope = provider.CreateScope();
//                 using var appContext = scope.ServiceProvider.GetRequiredService<WeggerTestContext>();
//                 appContext.Database.OpenConnection();
//                 appContext.Database.EnsureCreated();

//                 Seed(appContext);
//             });

//             builder.UseEnvironment("Integration");

//             return base.CreateHost(builder);
//         }

//         static void Seed(DbContext context)
//         {

//             DateTime _dateForFirstResource = DateTime.Now;


//             var users = new[] {
//                 new User { Id = "testUserId" },
//                 new User { Id = "secondUserId" }
//             };

//             var resources = new[] {
//                 new Resource {
//                     Id = 1,
//                     UserId = "testUserId",
//                     Title = "resource_1",
//                     SourceTitle = "some official title",
//                     Description = "test",
//                     TimeOfReference = _dateForFirstResource,
//                     Url = "https://github.com/wegger-BDSA2021/webapi/tree/develop",
//                     HostBaseUrl = "www.github.com",
//                     LastCheckedForDeprecation = _dateForFirstResource,
//                     IsVideo = false,
//                     IsOfficialDocumentation = false,
//                 },
//                 new Resource {
//                     Id = 2,
//                     UserId = "testUserId",
//                     Title = "resource_2",
//                     SourceTitle = "another official title",
//                     Description = "test of another",
//                     TimeOfReference = _dateForFirstResource,
//                     Url = "https://github.com/wegger-BDSA2021/webapi/tree/develop/test2",
//                     HostBaseUrl = "www.github.com",
//                     LastCheckedForDeprecation = _dateForFirstResource,
//                     IsVideo = false,
//                     IsOfficialDocumentation = false,
//                 }
//             };

//             var ratings = new[] {
//                 new Rating {
//                     Id = 1,
//                     Rated = 3,
//                     ResourceId = 1,
//                     UserId = "testUserId"
//                 },
//                 new Rating {
//                     Id = 2,
//                     Rated = 5,
//                     ResourceId = 1,
//                     UserId = "testUserId"
//                 },
//                 new Rating {
//                     Id = 3,
//                     Rated = 5,
//                     ResourceId = 2,
//                     UserId = "testUserId"
//                 }
//             };

//             var comments = new[] {
//                 new Comment {
//                     Id = 1,
//                     UserId = "testUserId",
//                     ResourceId = 1,
//                     TimeOfComment = DateTime.Now,
//                     Content = "Content description"
//                 }
//             };

//             var tags = new[] {
//                 new Tag { Id = 1, Name = "dotnet"},
//                 new Tag { Id = 2, Name = "linq"},
//             };

//             resources[0].Tags.Add(tags[0]);

//             context.AddRange(users);
//             context.AddRange(tags);
//             context.AddRange(ratings);
//             context.AddRange(resources);
//             context.AddRange(comments);

//             context.SaveChanges();
//         }


//     }
// }