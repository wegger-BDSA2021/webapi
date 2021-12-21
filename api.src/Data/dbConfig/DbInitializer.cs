using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Data
{
    public class DbInitializer
    {
        public static void Init(WeggerContext context, IServiceProvider services)
        {
            var logger = services.GetRequiredService<ILogger<DbInitializer>>();

            if (context.Resources.Any())
            {
                logger.LogInformation("The database was already seeded");
                return;
            }

            logger.LogInformation("Start seeding the database ...");

            var users = new[]
            {
                new User { Id = "first" },
                new User { Id = "second" },
                new User { Id = "third" },
            };

            var tags = new[]
            {
                new Tag {Name = "dotnet" },
                new Tag {Name = "c#" },
                new Tag {Name = "ef core" },
                new Tag {Name = "entity framework" },
                new Tag {Name = "entity framework core" },
                new Tag {Name = "c" },
                new Tag {Name = "c++" },
                new Tag {Name = "docker" },
                new Tag {Name = "github" },
                new Tag {Name = "blazor" },
                new Tag {Name = "uml" },
                new Tag {Name = "component diagram" },
                new Tag {Name = "DTO" },
                new Tag {Name = ".NET" },
                new Tag {Name = "aspnet" },
                new Tag {Name = "asp net" },
                new Tag {Name = "design pattern" },
                new Tag {Name = "design patterns" },
                new Tag {Name = "linq" },
                new Tag {Name = "ef" },
                new Tag {Name = "repository" },
                new Tag {Name = "class diagram" },
                new Tag {Name = "microsoft" },
                new Tag {Name = "deployment" },
                new Tag {Name = "framework" },
                new Tag {Name = "frameworks" },
                new Tag {Name = "docker swarm" },
                new Tag {Name = "containers" },
                new Tag {Name = "container" },
                new Tag {Name = "linux" },
                new Tag {Name = "mac" },
                new Tag {Name = "mac OS" },
                new Tag {Name = "microservice" },
                new Tag {Name = "microservices" },
                new Tag {Name = "github actions" },
                new Tag {Name = "continuous integration" },
                new Tag {Name = "continuous deployment" },
                new Tag {Name = "ci/cd" },
                new Tag {Name = "ci/cd pipeline" },
                new Tag {Name = "sequence diagram" },
                new Tag {Name = "collaboration diagram" },
                new Tag {Name = "use case diagram" },
                new Tag {Name = "bug" },
                new Tag {Name = "bugs" },
                new Tag {Name = "test" },
                new Tag {Name = "tdd" },
                new Tag {Name = "test environment" },
                new Tag {Name = "integation testing" },
                new Tag {Name = "integation test" },
                new Tag {Name = "integation tests" },
                new Tag {Name = "security" },
                new Tag {Name = "sql" },
                new Tag {Name = "database" },
                new Tag {Name = "databases" },
                new Tag {Name = "data annotations" },
                new Tag {Name = "ef migrations" },
                new Tag {Name = "migrations" },
                new Tag {Name = "database provider" },
                new Tag {Name = "database providers" },
                new Tag {Name = "data-access" },
                new Tag {Name = "open source" },
                new Tag {Name = "orm" },
                new Tag {Name = "query" },
                new Tag {Name = "querying" },
                new Tag {Name = "data" },
                new Tag {Name = "performance" },
                new Tag {Name = "sql server" },
            };

            var ratings = new[]
            {
                new Rating
                {
                    Rated = 3, 
                    UserId = "first"
                },
                new Rating
                {
                    Rated = 5, 
                    UserId = "second"
                },
                new Rating
                {
                    Rated = 3, 
                    UserId = "second"
                },
                new Rating
                {
                    Rated = 3, 
                    UserId = "first"
                },
                new Rating
                {
                    Rated = 2, 
                    UserId = "third"
                },
                new Rating
                {
                    Rated = 4, 
                    UserId = "third"
                },
                new Rating
                {
                    Rated = 2, 
                    UserId = "second"
                },
            };


            var resources = new[]
            {
                new Resource 
                {
                    
                    UserId = "first",
                    Title = "Component diagrams guide",
                    SourceTitle = "component", 
                    Description = "Awesome guide",
                    TimeOfReference = DateTime.Now,
                    Deprecated = false, 
                    HostBaseUrl = "www.uml-diagrams.org", 
                    Url = "https://www.uml-diagrams.org/component.html",
                    IsOfficialDocumentation = true, 
                    IsVideo = false, 
                    LastCheckedForDeprecation = DateTime.Now,
                },
                new Resource 
                {
                    
                    UserId = "second",
                    Title = "EF core guide",
                    SourceTitle = "overview of entity framework core", 
                    Description = "Get started on EF core",
                    TimeOfReference = DateTime.Now,
                    Deprecated = false, 
                    HostBaseUrl = "docs.microsoft.com", 
                    Url = "https://docs.microsoft.com/en-us/ef/core/",
                    IsOfficialDocumentation = true, 
                    IsVideo = false, 
                    LastCheckedForDeprecation = DateTime.Now,
                }, 
                new Resource 
                {
                    
                    UserId = "first",
                    Title = "Github Actions beginner guide",
                    SourceTitle = "github actions", 
                    Description = "Get started on Github Actions",
                    TimeOfReference = DateTime.Now,
                    Deprecated = false, 
                    HostBaseUrl = "docs.github.com", 
                    Url = "https://docs.github.com/en/actions",
                    IsOfficialDocumentation = false, 
                    IsVideo = false, 
                    LastCheckedForDeprecation = DateTime.Now,
                }, 
                new Resource 
                {
                    
                    UserId = "third",
                    Title = "LINQ",
                    SourceTitle = "language integrated query", 
                    Description = "Learn LINQ to ease queries in c#",
                    TimeOfReference = DateTime.Now,
                    Deprecated = false, 
                    HostBaseUrl = "docs.microsoft.com", 
                    Url = "https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/",
                    IsOfficialDocumentation = true, 
                    IsVideo = false, 
                    LastCheckedForDeprecation = DateTime.Now,
                }, 
                new Resource 
                {
                    
                    UserId = "third",
                    Title = "Learn brainfuck",
                    SourceTitle = "brainfuck", 
                    Description = "Learn brainfuck in 100 seconds",
                    TimeOfReference = DateTime.Now,
                    Deprecated = false, 
                    HostBaseUrl = "www.youtube.com", 
                    Url = "https://www.youtube.com/watch?v=hdHjjBS4cs8&ab_channel=Fireship",
                    IsOfficialDocumentation = false, 
                    IsVideo = true, 
                    LastCheckedForDeprecation = DateTime.Now,
                }, 
            };

            resources[0].Tags.Add(tags[11]);
            resources[0].Tags.Add(tags[10]);
            resources[0].Tags.Add(tags[5]);

            resources[1].Tags.Add(tags[2]);
            resources[1].Tags.Add(tags[8]);
            resources[1].Tags.Add(tags[13]);

            resources[2].Tags.Add(tags[8]);
            resources[2].Tags.Add(tags[20]);
            resources[2].Tags.Add(tags[24]);

            resources[3].Tags.Add(tags[2]);
            resources[3].Tags.Add(tags[1]);
            resources[3].Tags.Add(tags[18]);

            resources[4].Tags.Add(tags[5]);
            resources[4].Tags.Add(tags[8]);
            resources[4].Tags.Add(tags[20]);


            resources[0].Ratings.Add(ratings[0]);
            resources[0].Ratings.Add(ratings[1]);

            resources[1].Ratings.Add(ratings[2]);

            resources[2].Ratings.Add(ratings[3]);

            resources[3].Ratings.Add(ratings[4]);

            resources[4].Ratings.Add(ratings[5]);
            resources[4].Ratings.Add(ratings[6]);

            context.AddRange(users);
            context.AddRange(tags);
            context.AddRange(resources);
            context.AddRange(ratings);

            context.SaveChanges();

            logger.LogInformation("Finished seeding the database.");
        }
    }
}