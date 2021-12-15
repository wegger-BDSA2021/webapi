using api.src;
using Data;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace api.tests.Controller.Tests

{
    [Xunit.Collection("Sequential")]
    
    public class TagControllerTest : TestFixture
    {
        public TagControllerTest(WebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async void getAllTag_return_HttpStatusCode_OK()
        {
            var response = await Client.GetAsync("/api/Tag");
            Assert.Equal(HttpStatusCode.OK,response.StatusCode);
        }

        [Fact]
        public async void GetById_given_nonexisiting_id_returns_HttpStatusCode_NotFound()
        {
            int id = 42;
            var response = await Client.GetAsync($"/api/Tag/{id}");
            Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        }

        [Fact]
        public async void GetById_given_valid_Id_returns_HTTPStatusCode_OK()
        {
            var tagId = 1;
            //Act
            var response = await Client.GetAsync($"/api/Tag/{tagId}");
            //Assert
            Assert.Equal(HttpStatusCode.OK,response.StatusCode);

        }

        [Fact]
        public async void GetById_given_negative_Id_Returns_BadRequest()
        {
            var tagId = -1;
            //Act
            var response = await Client.GetAsync($"/api/Tag/{tagId}");
            //Assert
            Assert.Equal(HttpStatusCode.BadRequest,response.StatusCode);
        }

        [Fact]
        public async void Post_returns_HttpStatusCode_Created()
        {
            //Arrange
            var tag = new TagCreateDTO
            {
                Name = "testName",
            };
            
            JsonContent content = JsonContent.Create(tag);
            
            //Act
            var response = await Client.PostAsync("/api/Tag/Create",content);
            
            //Assert
            Assert.Equal(HttpStatusCode.Created,response.StatusCode);
        }

        [Fact]
        public async void Post_returns_HttpStatusCode_Conflict_already_existing_post()
        {
            //Arrange
            var tag = new TagCreateDTO
            {
                Name = "dotnet",
            };
            
            JsonContent content = JsonContent.Create(tag);
            
            //Act
            var conflictResponse = await Client.PostAsync("/api/Tag/Create", content);
            
            //Assert
            Assert.Equal(HttpStatusCode.Conflict,conflictResponse.StatusCode);
        }

        [Fact]
        public async void Put_returns_HttpStatusCode_OK()
        {
            //Arrange
            var tagUpdate = new TagUpdateDTO
            {
                Id = 1,
                NewName = "testNewName"
            };
            
            JsonContent contentUpdate = JsonContent.Create(tagUpdate);
            
            //Act
            var response = await Client.PutAsync($"/api/Tag/{tagUpdate.Id}",contentUpdate);
            
            //Assert
            Assert.Equal(HttpStatusCode.NoContent,response.StatusCode);
        }
        [Fact]
        public async void Put_returns_HttpStatusCode_NotFound()
        {
            //Arrange
            var tagUpdate = new TagUpdateDTO
            {
                Id = 42,
                NewName = "testNewName"
            };
            
            JsonContent contentUpdate = JsonContent.Create(tagUpdate);
            
            //Act
            var response = await Client.PutAsync($"/api/Tag/{tagUpdate.Id}",contentUpdate);
            
            //Assert
            Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        }
        [Fact]
        public async void Delete_returns_HttpStatusCode_NoContent()
        {
            //Arrange
            var tagId = 1;
            //Act
            var response = await Client.DeleteAsync($"/api/Tag/{tagId}");
            //Assert
            Assert.Equal(HttpStatusCode.NoContent,response.StatusCode);
        }
        [Fact]
        public async void Delete_returns_HttpStatusCode_NotFound()
        {
            //Arrange
            var tagId = 42;
            //Act
            var response = await Client.DeleteAsync($"/api/Tag/{tagId}");
            //Assert
            Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        }

    }
}