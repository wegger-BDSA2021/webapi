using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Utils;
using System.Security.Claims;
using Services;

namespace api.src.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService _service;
        static readonly string[] scopeRequiredByApi = new string[] { "ReadAccess" };

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> CreateUser()
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);

            // get the userId from microsoft graph
            string userId = this.User.GetUserId();

            var result = await _service.CreateAsync(userId);
            return result.ToActionResult();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(string id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _service.DeleteAsync(id);
            return result.ToActionResult();
        }
    }
}