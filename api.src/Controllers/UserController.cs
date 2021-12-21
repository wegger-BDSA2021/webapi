using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Utils;
using Services;

namespace Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
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
        [Route("Delete")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            HttpContext.VerifyUserHasAnyAcceptedScope(scopeRequiredByApi);
            var result = await _service.DeleteAsync(id);
            return result.ToActionResult();
        }
    }
}