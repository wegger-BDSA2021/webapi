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
                // new User { Id = 2, Name = "Paolo Tell", Email = "paolo@itu.dk"},
                // new User { Id = 3, Name = "Gustav Johansen", Email = "gujo@itu.dk", Tasks = new List<Task>(), },
            };

            var resources = new[] {
                new Resource { 
                    Id = 1,    
                    Title = "resource_1", 
                    Description = "test", 
                    UserId = 1,
                    TimeOfReference = _dateForFirstResource,
                    Url = "https://github.com/wegger-BDSA2021/webapi/tree/develop", 
                    LastCheckedForDeprecation = _dateForFirstResource
                }
            };

            var ratings = new [] {
                new Rating {
                    Id = 1,
                    Rated = 3,
                    ResourceId = 1,
                    UserId = 1
                }
            };

            var tags = new[] {
                new Tag { Id = 1, Name = "dotnet"},
                // new Tag { Id = 2, Name = "task can wait", Tasks = new List<Task>()},
            };

            // tags[1].Tasks.Add(tasks[1]);
            // tasks[1].Tags.Add(tags[1]);
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