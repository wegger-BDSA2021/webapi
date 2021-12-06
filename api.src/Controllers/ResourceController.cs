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

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<ResourceDetailsDTO>> CreateResource(ResourceCreateDTOClient resource)
        {
            var result = await _service.CreateAsync(resource);
            return result.ToActionResult();
        }

        // [HttpDelete("{id}")]
        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> DeleteResource(int id)
        {
            var result = await _service.DeleteByIdAsync(id);
            return result.ToActionResult();
        }

        // TODO : update resource endpoint ...


        // [Route("ReadSingle")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceDetailsDTO>> ReadSingleResource(int id)
        {
            var result = await _service.ReadAsync(id);
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("ReadAll")]
        public async Task<ActionResult<ICollection<ResourceDTO>>> ReadAllResources()
        {
            var result = await _service.ReadAllAsync();
            return result.ToActionResult();
        }

        // [Route("ReadAllFromUser")]
        [HttpGet("ReadAllFromUser/{id}")]
        public async Task<ActionResult<ICollection<ResourceDTO>>> ReadAllFromUser(int id)
        {
            var result = await _service.GetAllResourcesFromUserAsync(id);
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("ReadAllFromDomain")]
        public async Task<ActionResult<ICollection<ResourceDTO>>> ReadAllFromDomain([FromBody] string matcher)
        {
            var result = await _service.GetAllResourcesFromDomainAsync(matcher);
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("ReadAllWithAverageRatingInRange")]
        public async Task<ActionResult<ICollection<ResourceDTO>>> ReadAllWithAverageRatingInRange([FromBody] int from, int to)
        {
            var result = await _service.GetAllResourcesWithinRangeAsync(from, to);
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("ReadAllWithTitle")]
        public async Task<ActionResult<ICollection<ResourceDTO>>> ReadAllWhereTitleContians([FromBody] string matcher)
        {
            var result = await _service.GetAllResourcesWhereTitleContainsAsync(matcher);
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("ReadAllDeprecated")]
        public async Task<ActionResult<ICollection<ResourceDTO>>> ReadAllMarkedDeprecated()
        {
            var result = await _service.GetAllResourcesMarkedDeprecatedAsync();
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("ReadAllArticles")]
        public async Task<ActionResult<ICollection<ResourceDTO>>> ReadAllArticles()
        {
            var result = await _service.GetAllArticleResourcesAsync();
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("ReadAllVideos")]
        public async Task<ActionResult<ICollection<ResourceDTO>>> ReadAllVideos()
        {
            var result = await _service.GetAllVideoResourcesAsync();
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("ReadAllOfficialDocumentation")]
        public async Task<ActionResult<ICollection<ResourceDTO>>> ReadAllFromOfficialDocumentation()
        {
            var result = await _service.GetAllFromOfficialDocumentationAsync();
            return result.ToActionResult();
        }

        [HttpGet]
        [Route("ReadAllWithTags")]
        public async Task<ActionResult<ICollection<ResourceDTO>>> ReadAllWithProvidedTags([FromBody] ICollection<string> tags)
        {
            var result = await _service.GetAllResourcesWithProvidedTags(tags);
            return result.ToActionResult();
        }

    }
}