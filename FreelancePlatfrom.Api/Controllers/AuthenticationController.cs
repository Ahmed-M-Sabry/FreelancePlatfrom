using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.Command.Model;
using FreelancePlatfrom.Core.Features.Register.ClientRegister.Commands.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ApplicationControllerBase
    {
        [HttpPost]
        [Route("User-Register")]
        public async Task<IActionResult> RegisterUser([FromForm] AddClientCommand addClientCommand)
        {        
            var result = await Mediator.Send(addClientCommand);

            return NewResultStatusCode(result);
        }
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromForm] SiginInUserCommand siginInUserCommand)
        {
            var result = await Mediator.Send(siginInUserCommand);
            return NewResultStatusCode(result);
        }
    }
}
