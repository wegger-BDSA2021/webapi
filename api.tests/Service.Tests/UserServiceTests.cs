using Services;
using Data;
using Moq;
using Xunit;
using static Data.Response;

namespace api.tests.Service.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _userRepoMock = new();

        public UserServiceTests()
        {
            _userService = new UserService(_userRepoMock.Object);
        }

        [Fact]
        public async void CreateAsync_given_non_existing_id_returns_Created()
        {
            //Arrange
            _userRepoMock.Setup(u => u.CreateUserAsync("44")).ReturnsAsync((Created, "44"));

            //Act
            var actual = await _userService.CreateAsync("44");

            //Assert
            Assert.Equal(Created, actual.Response);
            Assert.Equal("User with id 44 has been created", actual.Message);
        }

        [Fact]
        public async void CreateAsync_given_existing_id_returns_BadRequest()
        {
            //Arrange
            _userRepoMock.Setup(u => u.CreateUserAsync("4")).ReturnsAsync((BadRequest, "4"));

            //Act
            var actual = await _userService.CreateAsync("4");

            //Assert
            Assert.Equal(BadRequest, actual.Response);
            Assert.Equal("User with id 4 already exists", actual.Message);
        }

        [Fact]
        public async void CreateAsync_given_empty_id_returns_BadRequest()
        {
            //Arrange
            _userRepoMock.Setup(u => u.CreateUserAsync("")).ReturnsAsync((BadRequest, ""));

            //Act
            var actual = await _userService.CreateAsync("");

            //Assert
            Assert.Equal(BadRequest, actual.Response);
            Assert.Equal("Id can not be the empty string", actual.Message);
        }

        [Fact]
        public async void DeleteAsync_given_non_existing_id_returns_NotFound()
        {
            //Arrange
            _userRepoMock.Setup(u => u.DeleteUserAsync("77")).ReturnsAsync(NotFound);

            //Act
            var actual = await _userService.DeleteAsync("77");

            //Assert
            Assert.Equal(NotFound, actual.Response);
            Assert.Equal("User with id 77 does not exists", actual.Message);
        }

        [Fact]
        public async void DeleteAsync_given_existing_id_returns_Deleted()
        {
            //Arrange
            _userRepoMock.Setup(u => u.DeleteUserAsync("7")).ReturnsAsync(Deleted);

            //Act
            var actual = await _userService.DeleteAsync("7");

            //Assert
            Assert.Equal(Deleted, actual.Response);
            Assert.Equal("User with id 7 has been deleted", actual.Message);
        }

        [Fact]
        public async void DeleteAsync_given_empty_id_returns_BadRequest()
        {
            //Arrange
            _userRepoMock.Setup(u => u.DeleteUserAsync("")).ReturnsAsync(BadRequest);

            //Act
            var actual = await _userService.DeleteAsync("");

            //Assert
            Assert.Equal(BadRequest, actual.Response);
            Assert.Equal("Id can not be the empty string", actual.Message);
        }
    }
}
