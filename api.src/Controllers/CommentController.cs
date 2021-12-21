using Services;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;

namespace Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;
        static readonly string[] scopeRequiredByApi = new string[] { "ReadAccess" };

        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> Get()
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await commentService.GetComments();
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDetailsDTO>> GetById(int id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await commentService.GetCommentById(id);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<CommentDetailsDTO>> CreateComment(CommentCreateDTOServer comment)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await commentService.AddComment(comment);
            return result.ToActionResult();
        }

        [HttpPut]
        public async Task<ActionResult<CommentDTO>> Put(CommentUpdateDTO comment)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var updatedUser = await commentService.UpdateComment(comment);
            return updatedUser.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CommentDTO>> Delete(int id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var commentToDelete = await commentService.DeleteComment(id);
            return commentToDelete.ToActionResult();
        }
    }
}