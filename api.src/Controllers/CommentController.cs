using api.src.Services;
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
        private readonly ICommentService commentService;

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> Get()
        {
            var result = await commentService.GetComments();
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDetailsDTO>> GetById(int id)
        {
            var result = await commentService.GetCommentById(id);

            if (result == null)
            {
                return NotFound();
            }

            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<CommentDetailsDTO>> CreateComment(CommentCreateDTOServer comment)
        {
            var result = await commentService.AddComment(comment);

            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CommentDTO>> UpdateComment(int id, CommentUpdateDTO comment)
        {
            var updatedUser = await commentService.UpdateComment(comment);

            return updatedUser.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CommentDTO>> DeleteComment(int id)
        {
            var commentToDelete = await commentService.DeleteComment(id);
            return commentToDelete.ToActionResult();
        }
    }
}