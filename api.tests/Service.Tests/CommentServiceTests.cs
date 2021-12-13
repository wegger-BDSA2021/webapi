using api.src.Controllers;
using api.src.Services;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Repository.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Data.Response;


namespace api.tests.Controller.Tests
{
    public class CommentServiceTests
    {

        private readonly CommentService _commentService;
        private readonly Mock<ICommentRepository> _commentRepoMock = new();

        public CommentServiceTests()
        {
            _commentService = new CommentService(_commentRepoMock.Object);
        }

        /*[Fact]
        public async Task Given_existing_commentId_getCommentById_returns_OK_and_the_first_comment()
        {
            //Arrange
            int commentId = 1;
            
            var commentDetailsDTO = new CommentDetailsDTO(commentId, "testUserId", 1, DateTime.Now, "Content of comment");

            _commentRepoMock.Setup(c => c.GetCommentById(commentId)).ReturnsAsync((OK, commentDetailsDTO));

            //Act
            var comment = await _commentService.GetCommentById(commentId);

            //Assert
            Assert.Equal(comment, _commentRepoMock);
        }*/

        [Fact]
        public async Task Delete_given_non_existing_returns_NotFound()
        {
            //Arrange
            _commentRepoMock.Setup(c => c.DeleteComment(42)).ReturnsAsync(NotFound);

            //Act
            var response = await _commentService.DeleteComment(42);

            //Assert
            Assert.Equal(NotFound, response.Response);
            Assert.Equal("No comment found with id 42", response.Message);
        }

        [Fact]
        public async Task Update_given_unknown_id_returns_NotFound()
        {
            // Arrange
            var updatedComment = new CommentUpdateDTO
            {
                Id = 9,
                UserId = "testUserId",
                ResourceId = 2,
                TimeOfComment = DateTime.Now,
                Content = "This is a updated comment!",
            };

            _commentRepoMock.Setup(c => c.UpdateComment(updatedComment)).ReturnsAsync(NotFound);

            // Act
            var response = await _commentService.UpdateComment(updatedComment);

            // Assert
            Assert.Equal(NotFound, response.Response);
            Assert.Equal("No comment found with the id 9", response.Message);
        }
    }
}
