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
        public async Task<ActionResult<ResourceDetailsDTO>> GetResource(int id)
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
    

    }
}