using Data;
using Moq;
using Services;
using System;
using System.Collections.Generic;
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

        //ReadAsync Tests

        [Fact]
        public async void ReadAsync_given_invalid_id__returns_BadRequest()
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
        public async void ReadAsync_given_no_entries_returns_NotFound()
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
            //Arrange
            var newResource = new ResourceDetailsDTO(7, "Title", "SourceTitle", "Description", DateTime.Now, "https://github.com/wegger-BDSA2021/webapi/tree/develop", "uml.org", null, null, 2, null, false, DateTime.Now, false, true);

            _resourceRepoMock.Setup(r => r.ReadAsync(7)).ReturnsAsync((OK, newResource));

            //Act
            var actual = await _resourceService.ReadAsync(7);

            //Assert
            Assert.Equal(OK, actual.Response);
            Assert.Equal("Resource found at index 7", actual.Message);
            Assert.NotNull(actual.DTO);
        }

        // Create Async Tests

        [Fact]
        public async void CreateAsync_given_existing_url_returns_Conflict()
        {
            //Arrange
            var resourceServer = new ResourceCreateDTOServer
            {
                TitleFromUser = "this is a new resource",
                UserId = "testUserId",
                Description = "description",
                TimeOfReference = DateTime.Now,
                Url = "https://github.com/wegger-BDSA2021/webapi/tree/develop",
                InitialRating = 4,
                Deprecated = false,
                LastCheckedForDeprecation = DateTime.Now
            };

            var resourceClient = new ResourceCreateDTOClient
            {
                Title = "this is a new resource",
                UserId = "testUserId",
                Description = "description",
                Url = "https://github.com/wegger-BDSA2021/webapi/tree/develop",
                InitialRating = 4
            };

            _tagRepoMock.Setup(s => s.GetAllTagsAsStringCollectionAsync()).ReturnsAsync(Array.Empty<string>());
            _resourceRepoMock.Setup(r => r.LinkExistsAsync("https://github.com/wegger-BDSA2021/webapi/tree/develop")).ReturnsAsync(true);
            _resourceRepoMock.Setup(r => r.CreateAsync(resourceServer)).ReturnsAsync((Conflict, null));

            //Act
            var actual = await _resourceService.CreateAsync(resourceClient);

            //Assert
            Assert.Equal(Conflict, actual.Response);
            Assert.Equal("Another resource with the same URL has already been provided", actual.Message);
            Assert.Null(actual.DTO);
        }

        [Fact]
        public async void CreateAsync_given_valid_ResourceCreateDTO_returns_Created()
        {
            //Arrange
            var returnedResource = new ResourceDetailsDTO(9, "Title", "SourceTitle", "Description", DateTime.Now, "https://github.com/wegger-BDSA2021/webapi/tree/develop", "uml.org", null, null, 2, null, false, DateTime.Now, false, true);

            var resourceServer = new ResourceCreateDTOServer
            {
                TitleFromUser = "this is a new resource",
                UserId = "testUserId",
                Description = "description",
                TimeOfReference = DateTime.Now,
                Url = "https://github.com/wegger-BDSA2021/webapi/tree/develop",
                InitialRating = 4,
                Deprecated = false,
                LastCheckedForDeprecation = DateTime.Now
            };

            var resourceClient = new ResourceCreateDTOClient
            {
                Title = "this is a new resource",
                UserId = "testUserId",
                Description = "description",
                Url = "https://github.com/wegger-BDSA2021/webapi/tree/develop",
                InitialRating = 4
            };

            _tagRepoMock.Setup(s => s.GetAllTagsAsStringCollectionAsync()).ReturnsAsync(Array.Empty<string>());

            _resourceRepoMock.Setup(r => r.CreateAsync(resourceServer)).ReturnsAsync((Created, returnedResource));

            //Act
            var actual = await _resourceService.CreateAsync(resourceClient);

            //Assert
            Assert.Equal(Created, actual.Response);
            Assert.Equal("A new resource was succesfully created", actual.Message);
            //Assert.NotNull(actual.DTO);
        }

        [Fact]
        public async void CreateAsync_given_invalid_url_returns_BadRequest()
        {
            //Arrange
            var resourceServer = new ResourceCreateDTOServer
            {
                TitleFromUser = "this is a new resource",
                UserId = "testUserId",
                Description = "description",
                TimeOfReference = DateTime.Now,
                Url = "http3fs://uml.org",
                InitialRating = 4,
                Deprecated = false,
                LastCheckedForDeprecation = DateTime.Now
            };

            var resourceClient = new ResourceCreateDTOClient
            {
                Title = "this is a new resource",
                UserId = "testUserId",
                Description = "description",
                Url = "http3fs://uml.org",
                InitialRating = 4
            };

            _resourceRepoMock.Setup(r => r.CreateAsync(resourceServer)).ReturnsAsync((BadRequest, null));

            //Act
            var actual = await _resourceService.CreateAsync(resourceClient);

            //Assert
            Assert.Equal(BadRequest, actual.Response);
            Assert.Equal("The provided URL is not valid", actual.Message);
            Assert.Null(actual.DTO);
        }

        /*[Fact]
        public async void CreateAsync_given_invalid_user_returns_Conflict()
        {
            //Arrange
            var resourceServer = new ResourceCreateDTOServer
            {
                TitleFromUser = "this is a new resource",
                UserId = "testUserId",
                Description = "description",
                TimeOfReference = DateTime.Now,
                Url = "https://github.com/wegger-BDSA2021/webapi/tree/develop",
                InitialRating = 4,
                Deprecated = false,
                LastCheckedForDeprecation = DateTime.Now
            };

            var resourceClient = new ResourceCreateDTOClient
            {
                Title = "this is a new resource",
                UserId = "testUserId",
                Description = "description",
                Url = "https://github.com/wegger-BDSA2021/webapi/tree/develop",
                InitialRating = 4
            };

            _tagRepoMock.Setup(s => s.GetAllTagsAsStringCollectionAsync()).ReturnsAsync(Array.Empty<string>());

            _resourceRepoMock.Setup(r => r.CreateAsync(resourceServer)).ReturnsAsync((NotFound, null));

            //Act
            var actual = await _resourceService.CreateAsync(resourceClient);

            //Assert
            Assert.Equal(Conflict, actual.Response);
            Assert.Equal("The user trying to create the resource does not exist in the current context", actual.Message);
            Assert.Null(actual.DTO);
        }*/

        // ReadAllAsync Tests

        [Fact]
        public async void ReadAllAsync_given_empty_DB_returns_readonlylist_of_length_0()
        {
            //Arrange
            _resourceRepoMock.Setup(r => r.ReadAllAsync()).ReturnsAsync(Array.Empty<ResourceDTO>());

            //Act
            var actual = await _resourceService.ReadAllAsync();

            //Assert
            Assert.Equal(OK, actual.Response);
        }

        // DeleteByIdAsync Tests

        [Fact]
        public async void DeleteByIdAsync_given_non_existing_id_returns_Notfound()
        {
            //Arrange
            _resourceRepoMock.Setup(r => r.DeleteAsync(18)).ReturnsAsync(NotFound);

            //Act
            var actual = await _resourceService.DeleteByIdAsync(18);

            //Assert
            Assert.Equal(NotFound, actual.Response);
            Assert.Equal("No resource found with id 18", actual.Message);
        }

        [Fact]
        public async void DeleteByIdAsync_given_existing_id_returns_Deleted()
        {
            //Arrange
            _resourceRepoMock.Setup(r => r.DeleteAsync(24)).ReturnsAsync(Deleted);

            //Act
            var actual = await _resourceService.DeleteByIdAsync(24);

            //Assert
            Assert.Equal(Deleted, actual.Response);
            Assert.Equal("Resource with id 24 has succesfully been deleted", actual.Message);
        }

        //UpdateResourceAsync Tests

        [Fact]
        public async void UpdateResourceAsync_given_unknown_id_returns_NotFound()
        {
            //Arrange
            var resource = new ResourceUpdateDTO
            {
                Id = 27,
                Title = "this is a new resource",
                UserId = "testUserId",
                Description = "description",
                TimeOfResourcePublication = DateTime.Now,
                Url = "https://github.com/wegger-BDSA2021/webapi/tree/develop",
                Deprecated = false,
                LastCheckedForDeprecation = DateTime.Now
            };

            _resourceRepoMock.Setup(r => r.UpdateAsync(resource)).ReturnsAsync(NotFound);

            //Act
            var actual = await _resourceService.UpdateResourceAsync(resource);

            //Assert
            Assert.Equal(NotFound, actual.Response);
            Assert.Equal("No resource found with the id 27", actual.Message);
        }


        [Fact]
        public async void UpdateResourceAsync_given_exsisting_id_returns_Updated()
        {
            //Arrange
            var resource = new ResourceUpdateDTO
            {
                Id = 14,
                Title = "this is a new resource",
                UserId = "testUserId",
                Description = "description",
                TimeOfResourcePublication = DateTime.Now,
                Url = "https://github.com/wegger-BDSA2021/webapi/tree/develop",
                Deprecated = false,
                LastCheckedForDeprecation = DateTime.Now
            };

            _resourceRepoMock.Setup(r => r.UpdateAsync(resource)).ReturnsAsync(Updated);

            //Act
            var actual = await _resourceService.UpdateResourceAsync(resource);

            //Assert
            Assert.Equal(Updated, actual.Response);
            Assert.Equal("Resource with id 14 has succefully been updated", actual.Message);
        }

        // GetAllResourcesFromUserAsync Tests

        [Fact]
        public async void GetAllResourcesFromUserAsync_given_id_with_length_0_returns_BadRequest()
        {
            //Arrange
            string id = "";

            //Act
            var actual = await _resourceService.GetAllResourcesFromUserAsync(id);

            //Assert
            Assert.Equal(BadRequest, actual.Response);
            Assert.Equal("Id can not be the empty string", actual.Message);
        }

        [Fact]
        public async void GetAllResourcesFromUserAsync_given_unknown_user_returns_NotFound()
        {
            //Arrange
            _userRepoMock.Setup(r => r.UserExists("33")).ReturnsAsync(false);

            //Act
            var actual = await _resourceService.GetAllResourcesFromUserAsync("33");

            //Assert
            Assert.Equal(NotFound, actual.Response);
            Assert.Equal("No user found with id 33", actual.Message);
        }

        [Fact]
        public async void GetAllResourcesFromUserAsync_given_valid_user_and_id_returns_OK()
        {
            //Arrange
            _userRepoMock.Setup(r => r.UserExists("57")).ReturnsAsync(true);
            _resourceRepoMock.Setup(r => r.GetAllFromUserAsync("57")).ReturnsAsync(Array.Empty<ResourceDTO>());

            //Act
            var actual = await _resourceService.GetAllResourcesFromUserAsync("57");

            //Assert
            Assert.Equal(OK, actual.Response);
            Assert.Equal("Collection with 0 resources found from user with id 57", actual.Message);
        }

        // GetAllResourcesFromDomainAsync Tests

        [Fact]
        public async void GetAllResourcesFromDomainAsync_given_empty_domain_returns_count_0()
        {
            //Arrange
            _resourceRepoMock.Setup(r => r.GetAllFromDomainAsync("domain")).ReturnsAsync(Array.Empty<ResourceDTO>());

            //Act
            var actual = await _resourceService.GetAllResourcesFromDomainAsync("domain");

            //Assert
            Assert.Equal(OK, actual.Response);
            Assert.Equal("Collection with 0 resources found from domain", actual.Message);
        }

        //GetAllResourcesWithinRangeAsync Tests

        [Fact]
        public async void GetAllResourcesWithinRangeAsync_given_empty_db_returns_count_0()
        {
            //Arrange
            _resourceRepoMock.Setup(r => r.GetAllWithRatingInRangeAsync(1, 2)).ReturnsAsync(Array.Empty<ResourceDTO>());

            //Act
            var actual = await _resourceService.GetAllResourcesWithinRangeAsync(1, 2);

            //Assert
            Assert.Equal(OK, actual.Response);
            Assert.Equal("Collection with 0 resources found with average rating between 1 and 2", actual.Message);
        }

        //GetAllResourcesWhereTitleContainsAsync Tests

        [Fact]
        public async void GetAllResourcesWhereTitleContainsAsync_given_unknown_title_returns_count_0()
        {
            //Arrange
            _resourceRepoMock.Setup(r => r.GetAllWhereTitleContainsAsync("Test")).ReturnsAsync(Array.Empty<ResourceDTO>());

            //Act
            var actual = await _resourceService.GetAllResourcesWhereTitleContainsAsync("Test");

            //Assert
            Assert.Equal(OK, actual.Response);
            Assert.Equal("Collection with 0 resources found where title contains Test", actual.Message);
        }

        //GetAllResourcesMarkedDeprecatedAsync Tests

        [Fact]
        public async void GetAllResourcesMarkedDeprecatedAsync_given_empty_db_returns_count_0()
        {
            //Arrange
            _resourceRepoMock.Setup(r => r.GetAllDeprecatedAsync()).ReturnsAsync(Array.Empty<ResourceDTO>());

            //Act
            var actual = await _resourceService.GetAllResourcesMarkedDeprecatedAsync();

            //Assert
            Assert.Equal(OK, actual.Response);
            Assert.Equal("Collection with 0 resources found, that has been marked as deprecated", actual.Message);
        }

        //GetAllArticleResourcesAsync Tests

        [Fact]
        public async void GetAllArticlesAsync_given_empty_db_returns_count_0()
        {
            //Arrange
            _resourceRepoMock.Setup(r => r.GetAllArticlesAsync()).ReturnsAsync(Array.Empty<ResourceDTO>());

            //Act
            var actual = await _resourceService.GetAllArticleResourcesAsync();

            //Assert
            Assert.Equal(OK, actual.Response);
            Assert.Equal("Collection with 0 article resources found", actual.Message);
        }

        //GetAllVideoResourcesAsync Tests

        [Fact]
        public async void GetAllVideoResourcesAsync_given_empty_db_returns_count_0()
        {
            //Arrange
            _resourceRepoMock.Setup(r => r.GetAllArticlesAsync()).ReturnsAsync(Array.Empty<ResourceDTO>());

            //Act
            var actual = await _resourceService.GetAllArticleResourcesAsync();

            //Assert
            Assert.Equal(OK, actual.Response);
            Assert.Equal("Collection with 0 video resources found", actual.Message);
        }

        // GetAllFromOfficialDocumentationAsync Tests

        [Fact]
        public async void GetAllFromOfficialDocumentationAsync_given_empty_db_returns_count_0()
        {
            //Arrange
            _resourceRepoMock.Setup(r => r.GetAllFromOfficialDocumentaionAsync()).ReturnsAsync(Array.Empty<ResourceDTO>());

            //Act
            var actual = await _resourceService.GetAllFromOfficialDocumentationAsync();

            //Assert
            Assert.Equal(OK, actual.Response);
            Assert.Equal("Collection with 0 resources found from official documentations", actual.Message);
        }

        // GetAllResourcesWithProvidedTags Tests

        [Fact]
        public async void GetAllResourcesWithProvidedTags_given_no_tags_returns_BadRequest()
        {
            //Arrange
            _resourceRepoMock.Setup(r => r.GetAllWithTagsAsyc(Array.Empty<string>())).ReturnsAsync(Array.Empty<ResourceDTO>());

            //Act
            var actual = await _resourceService.GetAllResourcesWithProvidedTags(Array.Empty<string>());

            //Assert
            Assert.Equal(BadRequest, actual.Response);
            Assert.Equal("You need to provide a minimum of one tag to search for", actual.Message);
        }
    }
}
