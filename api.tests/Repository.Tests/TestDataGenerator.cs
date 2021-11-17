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

        protected TestDataGenerator()
        {
            _connection = new SqliteConnection(_connectionString);
            _connection.Open();

            var options = new DbContextOptionsBuilder<WeggerContext>()
                              .UseSqlite(_connection)
                              .Options;

            _context = new WeggerContext(options);
            _context.Database.EnsureCreated();
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


            // var tasks = new[] {
            //     new Task { Id = 1, Title = "Complete assignment 4", Description = "Due date is this friday", State = Active, AssignedTo = 3},
            //     new Task { Id = 2, Title = "Vask op", Description = "I dag", State = Active, AssignedTo = 3},
            //     new Task { Id = 3, Title = "Hand in assignment 4", State = New,},
            // };

            // var tags = new[] {
            //     new Tag { Id = 1, Name = "task is urgent", Tasks = new List<Task>()},
            //     new Tag { Id = 2, Name = "task can wait", Tasks = new List<Task>()},
            // };

            // tags[1].Tasks.Add(tasks[1]);
            // tasks[1].Tags.Add(tags[1]);

            context.AddRange(users);
            // context.AddRange(tasks);
            // context.AddRange(tags);

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