using Data;
using static Data.Response;
using Microsoft.AspNetCore.Mvc;
using Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Utils
{
    public static class Extensions
    {
        public static ActionResult ToActionResult(this Result result) => result.Response switch
        {
            Updated         => new NoContentResult(),
            Deleted         => new NoContentResult(),
            NotFound        => new NotFoundObjectResult(result.Message),
            Conflict        => new ConflictObjectResult(result.Message),
            BadRequest      => new BadRequestObjectResult(result.Message),
            OK              => new OkObjectResult(result.DTO),
            Created         => new ObjectResult(result.DTO) { StatusCode = 201 }, 
            InternalError   => new ObjectResult(result.Message) { StatusCode = 500 },
            _               => throw new NotSupportedException($"{result.Response} not supported")
        };

        public static bool IsWithin(this double value, int minimum, int maximum)
            => value >= minimum && value <= maximum;

        public static CommentDTO AsCommentDTO(this Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                UserId = comment.UserId,
                ResourceId = comment.ResourceId,
                TimeOfComment = comment.TimeOfComment,
                Content = comment.Content
            };
        }

        public static CommentDetailsDTO AsCommentDetailsDTO(this Comment comment)
        {
            return new CommentDetailsDTO
            {
                Id = comment.Id,
                UserId = comment.UserId,
                ResourceId = comment.ResourceId,
                TimeOfComment = comment.TimeOfComment,
                Content = comment.Content
            };
        }

        public static CommentUpdateDTO AsCommentUpdateDTO(this Comment comment)
        {
            return new CommentUpdateDTO
            {
                Id = comment.Id,
                UserId = comment.UserId,
                ResourceId = comment.ResourceId,
                TimeOfComment = comment.TimeOfComment,
                Content = comment.Content,
            };
        }
      
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
      
    }
}