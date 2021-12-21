using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utils;
using Services;

namespace Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly ITagService _service;
        static readonly string[] scopeRequiredByApi = new string[] { "ReadAccess" };

        public TagController(ITagService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<TagDetailsDTO>>> getAllTags()
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _service.getAllTags();
            return result.ToActionResult();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TagDetailsDTO>> GetById(int id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _service.ReadAsync(id);
            return result.ToActionResult();
        }


        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<TagCreateDTO>> Create(TagCreateDTO tag)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _service.CreateAsync(tag);
            return result.ToActionResult();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(TagUpdateDTO tagUpdateDTO)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _service.UpdateAsync(tagUpdateDTO);
            return result.ToActionResult();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _service.Delete(id);
            return result.ToActionResult();
        }
    }
}
