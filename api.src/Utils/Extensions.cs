
using api.src.Data.DTOs;
using Data;
using Microsoft.AspNetCore.Mvc;

namespace Utils
{


    public static class Extensions
    {
        // public static IActionResult ToActionResult(this Status status) => status switch
        // {
        //     Updated => new NoContentResult(),
        //     Deleted => new NoContentResult(),
        //     NotFound => new NotFoundResult(),
        //     Conflict => new ConflictResult(),
        //     _ => throw new NotSupportedException($"{status} not supported")
        // };

        // public static ActionResult<T> ToActionResult<T>(this Option<T> option) where T : class
        //     => option.IsSome ? option.Value : new NotFoundResult();

        public static bool IsWithin(this double value, int minimum, int maximum)
            => value >= minimum && value <= maximum;

        public static CommentDTO AsCommentDTO(this Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                User = comment.User,
                Resource = comment.Resource,
                TimeOfComment = comment.TimeOfComment,
                Content = comment.Content
            };
        }
    }
}