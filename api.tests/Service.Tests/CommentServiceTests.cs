using api.src.Services;
using Data;
using Moq;
using System;
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

        [Fact]
        public async Task Given_existing_commentId_getCommentById_returns_OK_and_the_first_comment()
        {
            //Arrange
            int commentId = 1;
            
            var commentDetailsDTO = new CommentDetailsDTO(commentId, "testUserId", 1, DateTime.Now, "Content of comment");

            _commentRepoMock.Setup(c => c.GetCommentById(commentId)).ReturnsAsync((OK, commentDetailsDTO));

            //Act
            var comment = await _commentService.GetCommentById(commentId);

            //Assert
            Assert.Equal(OK, comment.Response);
            //Assert.Equal(commentDetailsDTO, comment.DTO.GetType());
        }

        [Fact]
        public async void Given_no_entries_getCommentById_returns_NotFound()
        {
            //Arrange
            int commentId = 10;

            _commentRepoMock.Setup(c => c.GetCommentById(commentId)).ReturnsAsync((NotFound, null));

            //Act
            var actual = await _commentService.GetCommentById(commentId);

            //Assert
            Assert.Equal(NotFound, actual.Response);
            Assert.Equal("No comment exists with the id 10", actual.Message);
            Assert.Null(actual.DTO);
        }

        [Fact]
        public async void Given_empty_db_getComments_returns_readonlylist_of_length_0()
        {
            //Arrange
            _commentRepoMock.Setup(c => c.GetComments()).ReturnsAsync(Array.Empty<CommentDTO>());

            //Act
            var empty = await _commentService.GetComments();

            //Assert
            Assert.Equal(OK, empty.Response);
        }

        [Fact]
        public async void Given_new_commentDTO_returns_Created_and_correct_DTO()
        {
            //Arrange
            var newComment = new CommentCreateDTOServer
            {
                UserId = "testUserId",
                ResourceId = 1,
                TimeOfComment = DateTime.Now,
                Content = "This is a new comment",
            };

            //Act
            var commentDetailsDTO = new CommentDetailsDTO(1, "testUserId", 1, DateTime.Now, "This is a new comment");
            _commentRepoMock.Setup(c => c.AddComment(newComment)).ReturnsAsync((OK, commentDetailsDTO));

            //Assert
            var result = await _commentService.AddComment(newComment);

            var response = result.Response;
            var createdDTO = result.DTO;

            Assert.Equal(Created, response);
        }

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
            Assert.Equal("No comment found with the id 2", response.Message);
        }
    }
}