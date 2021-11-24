using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> Comments()
        {
            try
            {
                var result = await commentRepository.GetComments();

                if (result != null)
                {
                    return result;
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
        public async Task<ActionResult<Comment>> GetById(int id)
        {
            try
            {
                var result = await commentRepository.GetCommentById(id);

                if (result != null)
                {
                    return result;
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
        public async Task<ActionResult<Comment>> Post(Comment comment)
        {
            try
            {
                if (comment == null)
                {
                    return BadRequest();
                }

                var result = await commentRepository.AddComment(comment);

                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data to the database");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Comment>> Put(int id, Comment comment)
        {
            try
            {
                if (id != comment.Id)
                {
                    return BadRequest("Id mismatch");
                }

                var result = await commentRepository.GetCommentById(id);

                if (result == null)
                {
                    return NotFound("Comment not found");
                }

                return await commentRepository.UpdateComment(comment);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data from the database");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> Delete(int id)
        {
            try
            {
                var result = await commentRepository.GetCommentById(id);

                if (result != null)
                {
                    return await commentRepository.DeleteComment(id);
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
