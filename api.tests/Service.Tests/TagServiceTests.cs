using api.src.Controllers;
using api.src.Services;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Repository.Tests;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Xunit;
using static Data.Response;


namespace api.tests.Service.Tests
{
    public class TagServiceTests
    {

        private readonly TagService _tagService;
        private readonly Mock<ITagRepository> _tagRepoMock = new();

        public TagServiceTests()
        {
            _tagService = new TagService(_tagRepoMock.Object);
        }

        [Fact]
        public async Task Given_negative_tagId_UpdateAsync_returns_BadRequest_and_message()
        {
            //Arrange
            int tagId = -1;
            
            var tagUpdateDTO = new TagUpdateDTO
            {
                Id = tagId,
                NewName = "testName"
            };

            _tagRepoMock.Setup(c => c.UpdateAsync(tagUpdateDTO)).ReturnsAsync(BadRequest);

            //Act
            var result = await _tagService.UpdateAsync(tagUpdateDTO);

            //Assert
            Assert.Equal(BadRequest, result.Response);
            Assert.Equal("Id can only be a positive integer",result.Message);
        }
        
        [Fact]
        public async Task Given_non_existing_tagId_UpdateAsync_returns_NotFound_and_message()
        {
            //Arrange
            int tagId = 42;
            
            var tagUpdateDTO = new TagUpdateDTO
            {
                Id = tagId,
                NewName = "testName"
            };

            _tagRepoMock.Setup(c => c.UpdateAsync(tagUpdateDTO)).ReturnsAsync(NotFound);

            //Act
            var result = await _tagService.UpdateAsync(tagUpdateDTO);

            //Assert
            Assert.Equal(NotFound, result.Response);
            Assert.Equal("No tag found with the given entity",result.Message);
        }
        
        [Fact]
        public async Task Given_valid_tagId_UpdateAsync_returns_Updated_and_message()
        {
            //Arrange
            int tagId = 1;
            
            var tagUpdateDTO = new TagUpdateDTO
            {
                Id = tagId,
                NewName = "testName"
            };

            _tagRepoMock.Setup(c => c.UpdateAsync(tagUpdateDTO)).ReturnsAsync(Updated);

            //Act
            var result = await _tagService.UpdateAsync(tagUpdateDTO);

            //Assert
            Assert.Equal(Updated, result.Response);
            Assert.Equal($"Tag at index {tagId} has been updated to have name {tagUpdateDTO.NewName}",result.Message);
        }
        
       [Fact]
        public async Task Given_already_existing_tagName_UpdateAsync_returns_BadRequest_and_message()
        {
            //Arrange
            IReadOnlyCollection<string> collection = null;
            var tagCreateDTO = new TagCreateDTO{Name = "testName"};
            var tagdetailsDTO = new TagDetailsDTO(1, "testName", collection);
            _tagRepoMock.Setup(c => c.CreateAsync(tagCreateDTO)).ReturnsAsync((Created,tagdetailsDTO));
            
            var tagUpdateDTO = new TagUpdateDTO
            {
                Id = 1,
                NewName = "testName"
            };

            _tagRepoMock.Setup(c => c.UpdateAsync(tagUpdateDTO)).ReturnsAsync(BadRequest);

            //Act
            var result = await _tagService.UpdateAsync(tagUpdateDTO);

            //Assert
            Assert.Equal(BadRequest, result.Response);
            Assert.Equal($"There already exists a tag with name {tagUpdateDTO.NewName}",result.Message);
        }

        [Fact]
        public async Task Delete_given_non_existing_returns_NotFound()
        {
            //Arrange
            _tagRepoMock.Setup(c => c.DeleteAsync(42)).ReturnsAsync(NotFound);

            //Act
            var response = await _tagService.Delete(42);

            //Assert
            Assert.Equal(NotFound, response.Response);
            Assert.Equal("No tag found with the given entity", response.Message);
        }

        [Fact]
        public async Task Delete_given_existing_returns_Deleted()
        {
            //Arrange
            _tagRepoMock.Setup(c => c.DeleteAsync(1)).ReturnsAsync(Deleted);
            
            //Act
            var response = await _tagService.Delete(1);
            
            //Assert
            Assert.Equal(Deleted,response.Response);
            Assert.Equal("Tag found at index 1",response.Message);
        }

        [Fact]
        public async Task ReadAsync_given_negative_id_returns_BadRequest()
        {
            //Arrange
            int tagId = -1;
            
            //Act
            var result = await _tagService.ReadAsync(tagId);

            //Assert
            Assert.Equal(BadRequest, result.Response);
            Assert.Equal("Id can only be a positive integer",result.Message);
        }
        [Fact]
        public async Task ReadAsync_given_non_existing_id_returns_NotFound()
        {
            //Arrange
            int tagId = 42;

            _tagRepoMock.Setup(c => c.GetTagByIdAsync(tagId)).ReturnsAsync((NotFound,null));

            //Act
            var result = await _tagService.ReadAsync(tagId);

            //Assert
            Assert.Equal(NotFound, result.Response);
            Assert.Equal("No tag found with the given entity",result.Message);
        }
        
        [Fact]
        public async Task ReadAsync_given_existing_id_returns_OK()
        {
            //Arrange
            int tagId = 1;
            IReadOnlyCollection<string> collection = null;

            var tagDetailsDTO = new TagDetailsDTO
            (
                tagId,"testname",collection
            );

            _tagRepoMock.Setup(c => c.GetTagByIdAsync(tagId)).ReturnsAsync((OK,tagDetailsDTO));

            //Act
            var result = await _tagService.ReadAsync(tagId);

            //Assert
            Assert.Equal(OK, result.Response);
            Assert.Equal($"Tag found at index {tagId}",result.Message);
            Assert.NotNull(result.DTO);
        }

        [Fact]
        public async Task CreateAsync_given_null_tag_returns_BadRequest()
        {
            //Arrange
            
            //Act
            var result = await _tagService.CreateAsync(null);
            
            //Assert
            Assert.Equal(BadRequest,result.Response);
            Assert.Equal("No tag given",result.Message);
        }

        [Fact]
        public async Task CreateAsync_Given_Allreadycreated_tag_returns_AllReadyExist()
        {
            //Arrange
            IReadOnlyCollection<string> collection = null;
            var tagCreateDTO = new TagCreateDTO{Name = "testName"};
            var tagdetailsDTO = new TagDetailsDTO(1, "testName", collection);
            
            _tagRepoMock.Setup(c => c.CreateAsync(tagCreateDTO)).ReturnsAsync((Created,tagdetailsDTO));
            _tagRepoMock.Setup(c => c.CreateAsync(tagCreateDTO)).ReturnsAsync((AllReadyExist,null));

            //Act
            var result = await _tagService.CreateAsync(tagCreateDTO);

            //Assert
            Assert.Equal(AllReadyExist, result.Response);
            Assert.Equal("The tag given already exist in the database",result.Message);
        }

        [Fact]
        public async Task CreateAsync_Given_valid_new_tag_returns_Created_and_DTO()
        {
            //Arrange
            IReadOnlyCollection<string> collection = null;
            var tagCreateDTO = new TagCreateDTO{Name = "testName"};
            var tagdetailsDTO = new TagDetailsDTO(1, "testName", collection);
            _tagRepoMock.Setup(c => c.CreateAsync(tagCreateDTO)).ReturnsAsync((Created,tagdetailsDTO));
            
            //Act
            var result = await _tagService.CreateAsync(tagCreateDTO);
            //Assert
            Assert.Equal(Created,result.Response);
            Assert.Equal("A new tag was successfully created", result.Message);
            Assert.NotNull(result.DTO);
        }

        [Fact]
        public async Task getAllTags_returnsalltags_from_repo()
        {
            //Arrange
            IReadOnlyCollection<string> tagDetailsDTOcollection = new ReadOnlyCollection<string>(new List<string>());
            var tagdetailsDTO = new TagDetailsDTO(1, "testName1", tagDetailsDTOcollection);
            var tagdetailsDTOSecond = new TagDetailsDTO(2, "testName2", tagDetailsDTOcollection);
            List<TagDetailsDTO> collection = new List<TagDetailsDTO>();
            collection.Add(tagdetailsDTO);
            collection.Add(tagdetailsDTOSecond);
            _tagRepoMock.Setup(c => c.GetAllTagsAsync()).ReturnsAsync(collection.AsReadOnly());
            
            //Act
            var result = await _tagService.getAllTags();
            
            //Assert
            Assert.Equal(OK,result.Response);
            Assert.NotNull(result.DTO);

        }
        
    }
}
