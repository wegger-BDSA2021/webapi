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
        protected readonly WeggerContext _context;

        protected readonly DateTime _dateForFirstResource;

        protected TestDataGenerator()
        {
            _connection = new SqliteConnection(_connectionString);
            _connection.Open();

            var options = new DbContextOptionsBuilder<WeggerContext>()
                              .UseSqlite(_connection)
                              .Options;

            _context = new WeggerContext(options);
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
                new User { Id = 1 },
            };

            var resources = new[] {
                new Resource { 
                    Id = 1,    
                    UserId = 1,
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
                    UserId = 1,
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
                    UserId = 1
                },
                new Rating {
                    Id = 2,
                    Rated = 5,
                    ResourceId = 1,
                    UserId = 1
                },
                new Rating {
                    Id = 3,
                    Rated = 5,
                    ResourceId = 2,
                    UserId = 1
                }
            };

            var tags = new[] {
                new Tag { Id = 1, Name = "dotnet"},
            };

            resources[0].Tags.Add(tags[0]);

            context.AddRange(users);
            context.AddRange(tags);
            context.AddRange(ratings);
            context.AddRange(resources);

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
}