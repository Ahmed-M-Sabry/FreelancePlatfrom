using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.UserSkillFreature.Command.Model;
using FreelancePlatfrom.Core.Features.UserSkillFreature.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Add if you want to restrict to authenticated users
    public class UserSkillsController : ApplicationControllerBase
    {
        [HttpGet("Get-Freelancer-Skills")]
        public async Task<IActionResult> GetFreelancerSkills()
        {
            var result = await Mediator.Send(new GetSkillsQuery());
            return NewResultStatusCode(result);
        }

        [HttpPost("Change-Skills")]
        public async Task<IActionResult> ChangeFreelancerSkills([FromForm] ChangeUserSkillsCommand changeSkillsCommand)
        {
            var result = await Mediator.Send(changeSkillsCommand);
            return NewResultStatusCode(result);
        }

        [HttpDelete("Delete-Skill/{skillId}")]
        public async Task<IActionResult> DeleteSkillById([FromRoute] int skillId)
        {
            var result = await Mediator.Send(new DeleteUserSkillByIdCommand { SkillId = skillId });
            return NewResultStatusCode(result);
        }
    }
}
