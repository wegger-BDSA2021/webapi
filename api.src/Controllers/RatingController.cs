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
    public class RatingController : ControllerBase
    {
        private IRatingService _service;

        public RatingController(IRatingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<Rating>>> RatingFromRec(int reId)
        {
            var result = await _service.ReadAllRatingFormRepositoryAsync(reId);
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetById(int id)
        {
            var result = await _service.ReadAsync(id);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<Rating>> Post(RatingCreateDTO rating)
        {
            var result = await _service.CreateAsync(rating);
            return result.ToActionResult();
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Put(RatingUpdateDTO ratingUpdateDTO)
        {
            var result = await _service.UpdateAsync(ratingUpdateDTO);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            var result = await _service.Delete(id);
            return result.ToActionResult();
        }
    }
}
