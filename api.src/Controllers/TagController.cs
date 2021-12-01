using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository tagRepository;

        public TagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
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
        public async Task<ActionResult<Rating>> GetById(int id)
        {
            try
            {
                var result = await tagRepository.GetTagByIdAsync(id);

                if (result.Tag != null)
                {
                    return Ok(result.Tag);
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
        public async Task<ActionResult<Tag>> GetfromRecourse(Resource Re)
        {
            try
            {
                var result = await tagRepository.GetAllTagsFormRepositoryAsync(Re);

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

        [HttpPost]
        public async Task<ActionResult<Tag>> Post(Tag tag)
        {
            try
            {
                if (tag == null)
                {
                    return BadRequest();
                }

                var result = await tagRepository.CreateAsync(tag);

                return CreatedAtAction(nameof(GetById), new { id = result.TagId}, result);

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data to the database");
            }
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
