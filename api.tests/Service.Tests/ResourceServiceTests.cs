using Data;
using Moq;
using Services;
using System;
using Xunit;
using static Data.Response;

namespace api.tests.Service.Tests
{
    public class ResourceServiceTests
    {
        private readonly ResourceService _resourceService;
        private readonly Mock<IResourceRepository> _resourceRepoMock = new();
        private readonly Mock<ITagRepository> _tagRepoMock = new();
        private readonly Mock<IUserRepository> _userRepoMock = new();

        public ResourceServiceTests()
        {
            _resourceService = new ResourceService(_resourceRepoMock.Object, _tagRepoMock.Object, _userRepoMock.Object);
        }

        [Fact]
        public async void ReadAsync_given_invalid_id__return_BadRequest()
        {
            //Arrange
            _resourceRepoMock.Setup(r => r.ReadAsync(-9)).ReturnsAsync((BadRequest, null));

            //Act
            var actual = await _resourceService.ReadAsync(-9);

            //Assert
            Assert.Equal(BadRequest, actual.Response);
            Assert.Equal("Id can only be a positive integer", actual.Message);
            Assert.Null(actual.DTO);
        }

        [Fact]
        public async void ReadAsync_given_no_entries_return_NotFound()
        {
            //Arrange
            _resourceRepoMock.Setup(r => r.ReadAsync(6)).ReturnsAsync((NotFound, null));

            //Act
            var actual = await _resourceService.ReadAsync(6);

            //Assert
            Assert.Equal(NotFound, actual.Response);
            Assert.Equal("No resource found with the given entity", actual.Message);
            Assert.Null(actual.DTO);
        }

        [Fact]
        public async void ReadAsync_given_valid_id_returns_OK()
        {
            var newResource = new ResourceDetailsDTO(7, "Title", "SourceTitle", "Description", DateTime.Now, "uml.org", "uml.org", null, null, 2, null, false, DateTime.Now, false, true);

            //Arrange
            _resourceRepoMock.Setup(r => r.ReadAsync(7)).ReturnsAsync((OK, newResource));

            //Act
            var actual = await _resourceService.ReadAsync(7);

            //Assert
            Assert.Equal(OK, actual.Response);
            Assert.Equal("Resource found at index 7", actual.Message);
            Assert.NotNull(actual.DTO);
        }





    }
}
