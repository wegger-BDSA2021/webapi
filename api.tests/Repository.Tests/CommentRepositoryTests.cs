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
            Assert.Equal(new DateTime(2021, 13, 12), comment.TimeOfComment);
            Assert.Equal(1, comment.Id);
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

        [Fact]
        public async void Given_new_commentDTO_returns_Created_and_correct_DTO()
        {
            var _repo = new CommentRepository(_context);
            Seed(_context);

            var newComment = new CommentCreateDTOServer
            {
                UserId = "testUserId",
                ResourceId = 1,
                TimeOfComment = DateTime.Now,
                Content = "This is a new comment",
            };

            var result = await _repo.AddComment(newComment);
            var response = result.Response;
            var createdDTO = result.comment;

            Assert.Equal(Created, response);
            Assert.Equal(2, createdDTO.Id);
            Assert.Equal("This is a new comment", createdDTO.Content);
            Assert.Equal(new DateTime(2021, 13, 12), createdDTO.TimeOfComment);
            Assert.Equal("testUserId", createdDTO.UserId);
            Assert.Equal(1, createdDTO.ResourceId);
        }

        [Fact]
        public async void Given_non_existing_commentId_deleteComment_returns_NotFound()
        {
            var _repo = new CommentRepository(_context);
            Seed(_context);

            var result = await _repo.DeleteComment(11);

            Assert.Equal(NotFound, result);
        }

        [Fact]
        public async void Given_existing_commentId_deleteComment_returns_Deleted()
        {
            var _repo = new CommentRepository(_context);
            Seed(_context);

            var result = await _repo.DeleteComment(1);

            Assert.Equal(Deleted, result);
        }

        [Fact]
        public async void Given_existing_commentId_updateComment_returns_Updated_and_correct_DTO()
        {
            var _repo = new CommentRepository(_context);
            Seed(_context);

            var updatedComment = new CommentUpdateDTO
            {
                Id = 1,
                UserId = "testUserId",
                ResourceId = 2,
                TimeOfComment = new DateTime(2021, 10, 15),
                Content = "This is a updated comment!",
            };

            var response = await _repo.UpdateComment(updatedComment);

            var actual = await _repo.GetCommentById(1);

            Assert.Equal(Updated, response);
            Assert.Equal(1, actual.comment.Id);
            Assert.Equal("This is a updated comment!", actual.comment.Content);
            Assert.Equal(new DateTime(2021, 10, 15), actual.comment.TimeOfComment);
            Assert.Equal("testUserId", actual.comment.UserId);
            Assert.Equal(2, actual.comment.ResourceId);
        }

        [Fact]
        public async void Given_non_existing_commentId_updateComment_returns_NotFound()
        {
            var _repo = new CommentRepository(_context);
            Seed(_context);

            var updatedComment = new CommentUpdateDTO
            {
                Id = 9,
                UserId = "testUserId",
                ResourceId = 2,
                TimeOfComment = DateTime.Now,
                Content = "This is a updated comment!",
            };

            var response = await _repo.UpdateComment(updatedComment);

            Assert.Equal(NotFound, response);
        }
    }
}
