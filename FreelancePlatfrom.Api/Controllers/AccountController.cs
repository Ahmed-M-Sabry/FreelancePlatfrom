using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.ChangePasswordFreature.Command.Model;
using FreelancePlatfrom.Core.Features.UserSkillFreature.Command.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ApplicationControllerBase
    {

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordCommand changePasswordCommand)
        {
            var result = await Mediator.Send(changePasswordCommand);
            return NewResultStatusCode(result);
        }


    }
}
