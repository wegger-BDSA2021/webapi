using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Data;
using static Data.Response;
using System.Collections.ObjectModel;

namespace Repository.Tests
{
    public class TagRepositoryTests : TestDataGenerator
    {
        [Fact]
        public async void Given_no_entries_returns_NotFound_tegbyid()
        {
            var _repo = new TagRepository(_context);

            var actual = await _repo.GetTagByIdAsync(1);
            
            Assert.Equal(NotFound, actual.Response);
            //Assert.Equal(null, actual.TagDetailsDTO);  //Do we do this       
        }
        [Fact]
        public async void Given_no_entries_returns_NotFound_update()
        {
            var _repo = new TagRepository(_context);
            
            var UTag = new TagUpdateDTO
            {
                Id = 1,
                NewName = "DotNet"
            };
            
            var actual = await _repo.UpdateAsync(UTag);
            
            Assert.Equal(NotFound, actual);
        }
        public async void Given_no_entries_returns_NotFound_creat()
        {
            var _repo = new TagRepository(_context);
            
            var CTag = new TagCreateDTO
            {
                Name = "DotNet"
            };
            
            var actual = await _repo.CreateAsync(CTag);
            
            Assert.Equal(NotFound, actual.Response);
            //Assert.Equal(null, actual.TagDetailsDTO);  //Do we do this       

        }
        [Fact]
        public async void Given_no_entries_returns_NotFound_delete()
        {
            var _repo = new TagRepository(_context);

            var actual = await _repo.DeleteAsync(1);
            
            Assert.Equal(NotFound, actual);
        }
        [Fact]
        public async void Given_valid_id_Delete_and_return_Deleted_and_Remove()
        {
            var _repo = new TagRepository(_context);
            Seed(_context);

            var actual = await _repo.DeleteAsync(1);
            var removed = await _repo.GetTagByIdAsync(1);

            Assert.Equal(Deleted, actual);
            Assert.Equal(NotFound, removed.Response);
            //Assert.Equal(null, removed.TagDetailsDTO);  //Do we do this       

        }
        [Fact]
        public async void Given_An_Empty_Repo_Returns_Null_GetAllTagsAsStringCollectionAsync()
        {
            var _repo = new TagRepository(_context);

            var actual = await _repo.GetAllTagsAsStringCollectionAsync();

            Assert.Equal(0 ,actual.Count());
        } 
        [Fact]
        public async void Given_An_Repo_with_data_Returns_Return_GetAllTagsAsStringCollectionAsync()
        {
            var _repo = new TagRepository(_context);
            Seed(_context);
            
            var tagStrings = new Collection<string>
            {
                "dotnet",
                "linq"
            };
            var actual = await _repo.GetAllTagsAsStringCollectionAsync();
            var tjeckOne = actual.Contains("dotnet");
            var tjeckTwo = actual.Contains("linq");

            Assert.Equal(tagStrings,actual); //read only might fuck me here
            Assert.Equal(tagStrings.Count,actual.Count);
            Assert.Equal(true,tjeckOne);
            Assert.Equal(true,tjeckTwo);
        } 

        [Fact]
        public async void Given_An_Entry_Returns_An_Ok()
        {
            var _repo = new TagRepository(_context);
            Seed(_context);

            var actual = await _repo.GetTagByIdAsync(1);

            var response = actual.Response;
            var Tag = actual.TagDetailsDTO;

            Assert.Equal(OK, response);
            Assert.Equal(1,Tag.Id);
            Assert.Equal("dotnet",Tag.Name );
        }
        [Fact]
        public async void Given_An_Empty_Repo_Returns_Null_GetAllTagsAsync()
        {
            var _repo = new TagRepository(_context);

            var actual = await _repo.GetAllTagsAsync();

            Assert.Empty(actual);
        } 
        
        [Fact]
        public async void Given_An_Repo_with_data_Returns_Return_GetAllTagsAsync()
        {
            var _repo = new TagRepository(_context);
            Seed(_context);

            var one = new TagDetailsDTO ( 1,"dotnet", null);
            var two = new TagDetailsDTO ( 2,"linq", null);

            var tags = new[] {
                new TagDetailsDTO ( 1,"dotnet", null),
                new TagDetailsDTO ( 2,"linq", null)
            };
            var actual = await _repo.GetAllTagsAsync();
            var tjeckOne = actual.Contains(one);
            var tjeckTwo = actual.Contains(two);

            Assert.Equal(tags.Count(),actual.Count());
            // Assert.Equal(true,tjeckOne);
            // Assert.Equal(true,tjeckTwo);
        } 
        [Fact]
        public async void Given_empty_db_readAllAsync_returns_readonlylist_of_length_0()
        {
            var _repo = new TagRepository(_context);

            var empty = await _repo.GetAllTagsAsync();
            Assert.Empty(empty);
        }
        [Fact]
        public async void Given_An_Repo_Create_a_Tag()
        {
            var _repo = new TagRepository(_context);
            Seed(_context);
            var CTag = new TagCreateDTO{
               Name = "xml"
            };
            var DTag = new TagDetailsDTO(
                3,
                "xml",
                new List<string>()
            );
            var actual = await _repo.CreateAsync(CTag);

            var itsReal = await _repo.GetTagByIdAsync(3);


            Assert.Equal(Created,actual.Response);
            Assert.Equal(OK,itsReal.Response);
        }
        [Fact]
        public async void Given_An_Repo_Update_a_Tag(){
            var _repo = new TagRepository(_context);
            Seed(_context);

            var UTag = new TagUpdateDTO{
                Id = 2,
                NewName = "SQL"
            };
            var actual = await _repo.UpdateAsync(UTag);
            var itsReal = await _repo.GetTagByIdAsync(2);

            Assert.Equal(Updated,actual);
            Assert.Equal(OK,itsReal.Response);
            Assert.Equal(UTag.NewName,itsReal.TagDetailsDTO.Name);
        }

        
    }
}