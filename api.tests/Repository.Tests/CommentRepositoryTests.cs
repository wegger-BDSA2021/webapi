using Data;
using Repository.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Utils;
using Xunit;
using static Data.Response;

namespace api.tests.Repository.Tests
{
    public class CommentRepositoryTests : TestDataGenerator
    {
        [Fact]
        public async void Given_no_entries_returns_NotFound()
        {
            var _repo = new CommentRepository(_context);

            var actual = await _repo.GetCommentById(1);

            Assert.Equal(actual.Response, NotFound);
        }

        [Fact]
        public async void Given_one_entry_returns_OK_and_the_first_comment()
        {
            var _repo = new CommentRepository(_context);
            Seed(_context);

            var actual = await _repo.GetCommentById(1);

            var response = actual.Response;

            var comment = actual.comment;

            Assert.Equal(OK, response);
            Assert.Equal("Content description", comment.Content);
            Assert.Equal(DateTime.Now, comment.TimeOfComment);
            Assert.Equal(1, comment.Id);
            Assert.Equal("resource_1", comment.Resource.Title);
            Assert.NotNull(comment.User);
            Assert.NotNull(comment.Resource);
        }

        [Fact]
        public async void Given_seededDB_readAllAsync_returns_readonlylist_of_length_1()
        {
            var _repo = new CommentRepository(_context);
            Seed(_context);

            var allComments = await _repo.GetComments();
            Assert.Equal(1, allComments.Count);
            Assert.Equal("Content description", allComments.First().Content);
        }

        [Fact]
        public async void Given_empty_db_readAllAsync_returns_readonlylist_of_length_0()
        {
            var _repo = new CommentRepository(_context);

            var empty = await _repo.GetComments();

            Assert.Equal(0, empty.Count);
        }
    }
}
