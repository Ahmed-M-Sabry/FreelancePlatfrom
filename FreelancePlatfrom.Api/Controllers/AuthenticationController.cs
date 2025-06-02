using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.ChangePasswordFreature.Command.Model;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.LogoutFeature.Command.Model;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.RefreshTokenFeature.Commnand.Models;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.Register.ClientRegister.Commands.Model;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.Register.FreelancerRegister.Model;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.SignInFeatures.Command.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ApplicationControllerBase
    {

        [HttpPost("Freelancer-Register")]
        public async Task<IActionResult> RegisterFreelancer([FromForm] AddFreelancerCommand addFreelancerCommand)
        {
            var result = await Mediator.Send(addFreelancerCommand);

            return NewResultStatusCode(result);
        }

        [HttpPost("User-Register")]
        public async Task<IActionResult> RegisterUser([FromForm] AddClientCommand addClientCommand)
        {        
            var result = await Mediator.Send(addClientCommand);

            return NewResultStatusCode(result);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromForm] SiginInUserCommand siginInUserCommand)
        {
            var result = await Mediator.Send(siginInUserCommand);
            if (result.Succeeded && result.Data?.RefreshToken != null && result.Data.CookieOptions != null)
            {
                Response.Cookies.Append("RefreshToken", result.Data.RefreshToken, result.Data.CookieOptions);
            }
            return NewResultStatusCode(result);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await Mediator.Send(new LogoutCommand());

            return NewResultStatusCode(result);
        }

        [HttpPost("Generate-New-token-From-RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var result = await Mediator.Send(new RefreshTokenCommand());

            if (!result.Succeeded)
                return NewResultStatusCode(result);

            if (result.Data?.RefreshToken != null && result.Data.CookieOptions != null)
            {
                Response.Cookies.Delete("RefreshToken");
                Response.Cookies.Append("RefreshToken", result.Data.RefreshToken, result.Data.CookieOptions);
            }
            return NewResultStatusCode(result);
        }
        [HttpPost("Generate-reset-token")]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateResetToken([FromForm] GenerateResetTokenCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }

        [HttpPut("Reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }

    }
}
