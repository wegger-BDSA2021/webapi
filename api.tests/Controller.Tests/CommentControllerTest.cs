using api.src.Controllers;
using api.src.Services;
using Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Data.Response;

namespace api.tests.Controller.Tests
{
    public class CommentControllerTest
    {
        /*[Fact]
        public async Task Update_given_unknown_id_returns_NotFound()
        {
            //Arrange
            var toCreate = new CommentUpdateDTO();
            var created = new CommentDetailsDTO(4, "testUserId", 6, DateTime.Now, "Content of comment");
            var repository = new Mock<ICommentRepository>();
            repository.Setup(c => c.UpdateComment(toCreate)).ReturnsAsync(NotFound);
            var controller = new CommentController(repository.Object);

            //Act
            var response = await controller.UpdateComment(toCreate);

            //Assert
            
        }*/

        /*private readonly Mock<ICommentRepository> _repository;
        private readonly Mock<ICommentService> _myService;
        private CommentController _controller;

        public CommentControllerTest()
        {
            _repository = new Mock<ICommentRepository>();
            _myService = new Mock<ICommentService>(_repository.Object);
            _controller = new CommentController(_myService.Object);
        }

        [Fact]
        public async Task Update_given_unknown_id_returns_NotFound()
        {
            //Arrange
            var toDelete = new CommentDetailsDTO(4, "testUserId", 6, DateTime.Now, "Content of comment");

            _myService.Setup(c => c.DeleteComment(4)).ReturnsAsync(new Services.Result
            {
                Response = NotFound,
                Message = $"No comment found with id 4"
            });

            //Act
            var response = await _controller.DeleteComment(4);

            //Assert

        }*/
    }
}
