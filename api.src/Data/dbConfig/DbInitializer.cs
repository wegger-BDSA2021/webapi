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
            // Get a logger
            var logger = services.GetRequiredService<ILogger<DbInitializer>>();

            // Make sure the database is created
            // We already did this in the previous step
            // context.Database.EnsureCreated();

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
                new Tag {Name = "linq" }
            };

            var ratings = new[]
            {
                new Rating
                {
                    // Id = 1,
                    Rated = 3, 
                    ResourceId = 1,
                    UserId = "first"
                },
                new Rating
                {
                    // Id = 2,
                    Rated = 5, 
                    ResourceId = 1,
                    UserId = "second"
                }
            };

            // var comments = new[]
            // {
            //     new Comment
            //     {
            //         // Id = 1,
            //         UserId = "third",
            //         ResourceId = 1,
            //         TimeOfComment = DateTime.Now,
            //         Content = "First comment from user with id third"
            //     }
            // };

            var resources = new[]
            {
                new Resource 
                {
                    // Id = 1,
                    UserId = "first",
                    Title = "Something nice",
                    SourceTitle = "cool", 
                    Description = "test",
                    TimeOfReference = DateTime.Now,
                    Deprecated = false, 
                    HostBaseUrl = "www.uml-diagrams.org", 
                    Url = "https://www.uml-diagrams.org/component.html",
                    IsOfficialDocumentation = true, 
                    IsVideo = false, 
                    LastCheckedForDeprecation = DateTime.Now,
                }
            };

            resources[0].Tags.Add(tags[11]);
            resources[0].Tags.Add(tags[10]);
            resources[0].Tags.Add(tags[5]);

            context.AddRange(users);
            context.AddRange(tags);
            context.AddRange(resources);
            context.AddRange(ratings);
            // context.AddRange(comments);

            context.SaveChanges();

            logger.LogInformation("Finished seeding the database.");
        }
    }
}