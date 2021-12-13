// using api.src.Data.DTOs;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;

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
        public async Task<ActionResult<IEnumerable<Comment>>> Get()
        {
            try
            {
                var result = await commentRepository.GetComments();

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CommentDTO>> GetById(int id)
        {
            try
            {
                var result = await commentRepository.GetCommentById(id);

                if (result.comment == null)
                {
                    return NotFound();
                }

                return result.comment.AsCommentDTO();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CommentDTO>> Post(Comment comment)
        {
            try
            {
                if (comment == null)
                {
                    return BadRequest();
                }

                var result = await commentRepository.AddComment(comment);

                return CreatedAtAction(nameof(GetById), new { id = result.comment.Id }, result.comment.AsCommentDTO());

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data to the database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CommentDTO>> Put(int id, Comment comment)
        {
            try
            {
                if (id != comment.Id)
                {
                    return BadRequest("Comment id mismatch");
                }

                var result = await commentRepository.GetCommentById(id);

                if (result.comment == null)
                {
                    return NotFound("Comment not found");
                }

                var updatedUser = await commentRepository.UpdateComment(comment);

                return updatedUser.comment.AsCommentDTO();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data from the database");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CommentDTO>> Delete(int id)
        {
            try
            {
                var commentToDelete = await commentRepository.GetCommentById(id);

                if (commentToDelete.comment == null)
                {
                    return NotFound("Comment not found");
                }

                var deletedComment = await commentRepository.DeleteComment(id);

                return deletedComment.comment.AsCommentDTO();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }
    }
}