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
            try
            {
                var result = await tagRepository.GetAllTagsAsync();

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
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
        public async Task<ActionResult<Response>> Put(int id, string newTagName)
        {
            try
            {
                /*if (id != rating.Id)
                {
                    return BadRequest("Id mismatch");
                }*/

                var result = await tagRepository.GetTagByIdAsync(id);

                if (result.Tag == null)
                {
                    return NotFound("Comment not found");
                }

                return await tagRepository.UpdateAsync(result.Tag, newTagName);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data from the database");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            try
            {
                var result = await tagRepository.GetTagByIdAsync(id);

                if (result.Tag != null)
                {
                    return await tagRepository.DeleteAsync(id);
                }
                else
                {
                    return NotFound("Comment not found");
                }
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }
    }
}
