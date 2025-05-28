using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.AccountFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.AccountFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.AccountFeatures.Queries.Results;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.ChangePasswordFreature.Command.Model;
using FreelancePlatfrom.Core.Features.UserSkillFreature.Command.Model;
using FreelancePlatfrom.Data.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpPut("Update-Name")]
        public async Task<IActionResult> UpdateName([FromForm] EditUserNameCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        [HttpGet("Freelancer-profile")]
        public async Task<IActionResult> GetFreelancerProfile()
        {
            var result = await Mediator.Send(new GetFreelancerProfileQuery());
            return NewResultStatusCode(result);
        }
        [Authorize(Roles = ApplicationRoles.User)]
        [HttpGet("User-profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var result = await Mediator.Send(new GetUserProfileQuery());
            return NewResultStatusCode(result);
        }
        [HttpPut("Freelancer/edit-professional-info")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> EditFreelancerProfessionalInfo([FromForm] EditFreelancerProfessionalInfoCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }
        [HttpPut("Freelancer/edit-location")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> EditFreelancerLocation([FromForm] EditFreelancerLocationCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }
        [HttpPut("Freelancer/edit-general-info")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> EditFreelancerGeneralInfo([FromForm] EditFreelancerGeneralInfoCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        [HttpPut("update-skills")]
        public async Task<IActionResult> UpdateSkills([FromForm] EditFreelancerSkillsCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        [HttpPut("Update-languages")]
        public async Task<IActionResult> UpdateLanguages([FromForm] EditFreelancerLanguagesCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }



    }
}
