using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.SkillsFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.SkillsFeatures.Queries.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = ApplicationRoles.Admin)]
    public class SkillController : ApplicationControllerBase
    {
        [HttpPost("Create-New-Skill")]
        public async Task<IActionResult> CreateNewSkill([FromForm] string skillName)
        {
            var response = await Mediator.Send(new CreateSkillCommand(skillName));
            return NewResultStatusCode(response);
        }
        [HttpPut("Edit-Skill")]
        public async Task<IActionResult> EditSkill([FromForm] EditSkillCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResultStatusCode(response);
        }
        [HttpPut("Delete-Skill")]
        public async Task<IActionResult> DeleteSkill([FromForm] int id)
        {
            var response = await Mediator.Send(new DeleteSkillCommand(id));
            return NewResultStatusCode(response);
        }
        [HttpPut("Restore-Skill")]
        public async Task<IActionResult> RestoreSkill([FromForm] int id)
        {
            var response = await Mediator.Send(new RestoreSkillCommand(id));
            return NewResultStatusCode(response);
        }
        [HttpGet("Get-All-Skills-For-Admin")]
        public async Task<IActionResult> GetAllSkillsForAdmin()
        {
            var response = await Mediator.Send(new GetAllSkillsForAdminQuery());
            return NewResultStatusCode(response);
        }
        [HttpGet("Get-All-Skills-For-User")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllSkillsForUser()
        {
            var response = await Mediator.Send(new GetAllSkillsForUserQuery());
            return NewResultStatusCode(response);
        }
    }
}
