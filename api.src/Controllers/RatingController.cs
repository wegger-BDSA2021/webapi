using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        private readonly IRatingRepository ratingRepository;

        public RatingController(IRatingRepository ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<Rating>>> RatingFromRec(int reId)
        {
            try
            {
                var result = await ratingRepository.GetAllRatingFormRepositoryAsync(reId);

                if (result != null)
                {
                    return Ok(result);
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
        public async Task<ActionResult<Rating>> GetById(int id)
        {
            try
            {
                var result = await ratingRepository.ReadAsync(id);

                if (result.Rating != null)
                {
                    return result.Rating;
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

        [HttpGet("{id}{id}")]
        public async Task<ActionResult<Rating>> GetById(int Uid, int Rid)
        {
            try
            {
                var result = await ratingRepository.ReadAsync(Uid,Rid);

                if (result.Rating != null)
                {
                    return result.Rating;
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
        public async Task<ActionResult<Rating>> Post(Rating rating)
        {
            try
            {
                if (rating == null)
                {
                    return BadRequest();
                }

                var result = await ratingRepository.CreateAsync(rating);

                return CreatedAtAction(nameof(GetById), new { id = result.RatingId }, result);

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data to the database");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> Put(int id, Rating rating, int newRate)
        {
            try
            {
                if (id != rating.Id)
                {
                    return BadRequest("Id mismatch");
                }

                var result = await ratingRepository.ReadAsync(id);

                if (result.Rating == null)
                {
                    return NotFound("Comment not found");
                }

                return await ratingRepository.UpdateAsync(result.Rating, newRate);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data from the database");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            try
            {
                var result = await ratingRepository.ReadAsync(id);

                if (result.Rating != null)
                {
                    return await ratingRepository.DeleteAsync(id);
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
