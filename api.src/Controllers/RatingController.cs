using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;

namespace api.src.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private IRatingService _service;
        static readonly string[] scopeRequiredByApi = new string[] { "ReadAccess" };

        public RatingController(IRatingService service)
        {
            _service = service;
        }

        [HttpGet("{resourceId}")]
        [Route("Resource")]
        public async Task<ActionResult<IReadOnlyCollection<Rating>>> RatingsFromResource(int resourceId)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _service.ReadAllRatingFormRepositoryAsync(resourceId);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetById(int id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _service.ReadAsync(id);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<Rating>> Post(RatingCreateDTO rating)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _service.CreateAsync(rating);
            return result.ToActionResult();
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Put(RatingUpdateDTO ratingUpdateDTO)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _service.UpdateAsync(ratingUpdateDTO);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _service.Delete(id);
            return result.ToActionResult();
        }
    }
}
