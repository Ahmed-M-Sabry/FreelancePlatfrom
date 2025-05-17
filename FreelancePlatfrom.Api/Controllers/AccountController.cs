using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.ChangePasswordFreature.Command.Model;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.ChangeSkillsFeatures.Command.Model;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.GetSkillsFeatures.Query.Model;
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
        [HttpGet("Get-Freelancer-Skills")]
        public async Task<IActionResult> GetFreelancerSkills()
        {
            var result = await Mediator.Send(new GetSkillsQuery());
            return NewResultStatusCode(result);
        }
        [HttpPost("Change-Skills")]
        public async Task<IActionResult> ChangeFreelancerSkills([FromForm] ChangeSkillsCommand changeSkillsCommand)
        {
            var result = await Mediator.Send(changeSkillsCommand);
            return NewResultStatusCode(result);
        }

        [HttpDelete("Delete-Skill/{skillId}")]
        public async Task<IActionResult> DeleteSkillById(int skillId)
        {
            var result = await Mediator.Send(new DeleteSkillByIdCommand { SkillId = skillId });
            return NewResultStatusCode(result);
        }

    }
}
