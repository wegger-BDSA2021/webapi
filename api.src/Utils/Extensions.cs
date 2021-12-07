using static Data.Response;
using Microsoft.AspNetCore.Mvc;
using Services;
using Microsoft.AspNetCore.Http;
using System;

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

        // public static ActionResult<T> ToActionResult<T>(this Option<T> option) where T : class
        //     => option.IsSome ? option.Value : new NotFoundResult();

        public static bool IsWithin(this double value, int minimum, int maximum)
            => value >= minimum && value <= maximum;

    }
}