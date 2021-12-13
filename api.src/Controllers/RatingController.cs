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
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rating>> GetById(int id)
        {
            var result = await _service.ReadAsync(id);
            return result.ToActionResult();
        }

        // [HttpGet("{id}{id}")]
        // public async Task<ActionResult<Rating>> GetById(int Uid, int Rid)
        // {
        //     try
        //     {
        //         var result = await ratingRepository.ReadAsync(Uid,Rid);

        //         if (result.Rating != null)
        //         {
        //             return result.Rating;
        //         }
        //         else
        //         {
        //             return NotFound();
        //         }
        //     }
        //     catch (System.Exception)
        //     {
        //         return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
        //     }
        // }

        [HttpPost]
        public async Task<ActionResult<Rating>> Post(RatingCreateDTO rating)
        {
            var result = await _service.CreateAsync(rating);
            return result.ToActionResult();
        }

        [HttpPut("{id}")]
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
