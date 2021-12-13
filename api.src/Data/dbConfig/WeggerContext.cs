
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Data
{
    public class WeggerContext : DbContext, IWeggerContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Resource> Resources => Set<Resource>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Comment> Comments => Set<Comment>();


        public WeggerContext(DbContextOptions<WeggerContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
           
            //builder.ApplyConfigurationsFromAssembly(typeof(WeggerContext).Assembly);
            builder.Entity<User>().HasMany(u => u.Ratings).WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>().HasMany(u => u.Resources).WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<User>().HasMany(u => u.Comments).WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                

            // builder.Entity<User>().HasData(
            //     new User { Id = "first" },
            //     new User { Id = "second" }, 
            //     new User { Id = "third" }
            // );

            // builder.Entity<Tag>().HasData(
            //     new Tag { Id = 1, Name = "dotnet" },
            //     new Tag { Id = 2, Name = "c#" },
            //     new Tag { Id = 3, Name = "ef core" },
            //     new Tag { Id = 4, Name = "entity framework" },
            //     new Tag { Id = 5, Name = "entity framework core" },
            //     new Tag { Id = 6, Name = "c" },
            //     new Tag { Id = 7, Name = "c++" },
            //     new Tag { Id = 8, Name = "docker" },
            //     new Tag { Id = 9, Name = "github" },
            //     new Tag { Id = 10, Name = "blazor" },
            //     new Tag { Id = 11, Name = "uml" },
            //     new Tag { Id = 12, Name = "component diagram" },
            //     new Tag { Id = 13, Name = "DTO" },
            //     new Tag { Id = 14, Name = ".NET" },
            //     new Tag { Id = 15, Name = "aspnet" },
            //     new Tag { Id = 16, Name = "asp net" },
            //     new Tag { Id = 17, Name = "design pattern" },
            //     new Tag { Id = 18, Name = "design patterns" },
            //     new Tag { Id = 19, Name = "linq" }
            // );

            // builder.Entity<Resource>().HasData(
            //     new Resource {
            //         Id = 1,
            //         UserId = "first",
            //         Title = "Component diagrams from official guide",
            //         Description = "uml-diagrams.org official guide to make neat component diagrams",
            //         HostBaseUrl = "www.uml-diagrams.org",
            //         Url = "https://www.uml-diagrams.org/component.html",
            //         IsVideo = false, 
            //         IsOfficialDocumentation = true, 
            //         Deprecated = false, 
            //         SourceTitle = "UML",
            //         LastCheckedForDeprecation = System.DateTime.Now,
            //         TimeOfReference = System.DateTime.Now,
            //         Comments = null, 
            //         Tags = null, 
            //         Ratings = null, 
            //     }
            // );

        
        }

    }
}