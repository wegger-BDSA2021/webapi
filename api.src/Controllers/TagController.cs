using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;

namespace api.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private ITagService _service;
        private readonly ITagRepository tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }
        public TagController(ITagService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<Tag>>> getAllTags()
        {
            var result = await _service.getAllTags();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagDetailsDTO>> GetById(int id)
        {
             var result = await _service.ReadAsync(id);
            return result.ToActionResult();
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<Tag>> GetfromRecourse(Resource Re)
        // {
        //     try
        //     {
        //         var result = await tagRepository.GetAllTagsFormRepositoryAsync(Re);

        //         if (result != null)
        //         {
        //             return Ok(result);
        //         }
        //         else
        //         {
        //             return NotFound();
        //         }
        //     }
        //     catch (System.Exception)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
        //     }
        // }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<TagCreateDTO>> Post(TagCreateDTO tag)
        {
            var result = await _service.CreateAsync(tag);
            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> Put(TagUpdateDTO tagUpdateDTO)
        {
            var result = await _service.UpdateAsync(tagUpdateDTO);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            var result = await _service.Delete(id);
            return result.ToActionResult();
        }
    }
}
