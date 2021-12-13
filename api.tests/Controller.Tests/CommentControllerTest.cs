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
    /*public class CommentControllerTest
    {
        [Fact]
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
            
        }
    }*/
}
