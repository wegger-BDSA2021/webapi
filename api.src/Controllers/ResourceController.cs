using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Utils;

namespace api.src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourceController : ControllerBase
    {
        private IResourceService _service;

        public ResourceController(IResourceService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceDetailsDTO>> ReadSingleResource(int id)
        {
            var result = await _service.ReadAsync(id);
            return result.ToActionResult();
        }

        [HttpPost]
        public async Task<ActionResult<ResourceDetailsDTO>> CreateResource(ResourceCreateDTOClient resource)
        {
            var result = await _service.CreateAsync(resource);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteResource(int id)
        {
            var result = await _service.DeleteByIdAsync(id);
            return result.ToActionResult();
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ResourceDTO>>> ReadAllResources()
        {
            var result = await _service.ReadAllAsync();
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ICollection<ResourceDTO>>> GetAllFromUser(int id)
        {
            var result = await _service.GetAllResourcesFromUserAsync(id);
            return result.ToActionResult();
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ResourceDTO>>> ReadAllFromDomain([FromBody] string matcher)
        {
            var result = await _service.GetAllResourcesFromDomainAsync(matcher);
            return result.ToActionResult();
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ResourceDTO>>> ReadAllWithRatingInRange([FromBody] int from, [FromBody] int to)
        {
            var result = await _service.GetAllResourcesWithinRangeAsync(from, to);
            return result.ToActionResult();
        }

        
    

    }
}