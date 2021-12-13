using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Repository.Tests
{
    [Xunit.Collection("Sequential")]
    public abstract class TestDataGenerator : IDisposable
    {
        private const string _connectionString = "DataSource=:memory";

        private readonly SqliteConnection _connection;
        protected readonly WeggerTestContext _context;

        protected readonly DateTime _dateForFirstResource;

        protected TestDataGenerator()
        {
            // var connectionStringBuilder = new SqliteConnectionStringBuilder
            // { DataSource = ":memory:" };
            // var connectionString = connectionStringBuilder.ToString();

            // _connection = new SqliteConnection(_connectionString);
            // _connection.Open();

            _connection = new SqliteConnection(_connectionString);
            _connection.Open();

            var options = new DbContextOptionsBuilder<WeggerTestContext>()
                              .UseSqlite(_connection)
                              .Options;

            _context = new WeggerTestContext(options);
            _context.Database.EnsureCreated();

            _dateForFirstResource = DateTime.Now;
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
            _connection.Close();
        }

        protected void Seed(DbContext context)
        {
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

        [Fact]
        public async void Db_available() => Assert.True(await _context.Database.CanConnectAsync());

        [Fact]
        public void should_produce_true_with_seed_method()
        {
            Seed(_context);
            Assert.True(_context.Users.Any());
        }

        [Fact]
        public void should_produce_false_without_seed_method()
        {
            Assert.False(_context.Users.Any());
        }
    }

    public class WeggerTestContext : DbContext, IWeggerContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Resource> Resources => Set<Resource>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<Comment> Comments => Set<Comment>();


        public WeggerTestContext(DbContextOptions<WeggerTestContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // ... the seeding method will handle this
            builder.Entity<User>().HasMany(u => u.Ratings).WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>().HasMany(u => u.Resources).WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<User>().HasMany(u => u.Comments).WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
        }

    }
}