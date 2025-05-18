using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.JobPostFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Command.Dtos;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Command.Models;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Queries.Models;
using FreelancePlatfrom.Data.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FreelancePlatfrom.Core.Features.JopPostFeatrures.Command.Models;
using FreelancePlatfrom.Core.Features.JopPostFeatrures.Queries.Models;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ApplicationRoles.User)]
    public class jobPostController : ApplicationControllerBase
    {
        [AllowAnonymous]
        [HttpGet("Get-All-Job-Posts")]
        public async Task<IActionResult> GetAllJobPosts()
        {
            var result = await Mediator.Send(new GetAllJobPostQuery());
            return NewResultStatusCode(result);
        }
        [HttpGet("Get-My-Job-Posts")]
        public async Task<IActionResult> GetMyJobPosts()
        {
            var result = await Mediator.Send(new GetMyJobPostQuery());
            return NewResultStatusCode(result);
        }
        [HttpGet("Get-Job-Post-By-Id/{id}")]
        public async Task<IActionResult> GetjobPostById(int id)
        {
            var result = await Mediator.Send(new GetJobPostByIdQuery(id));
            return NewResultStatusCode(result);
        }
        [HttpPost("Create-Job-Post")]
        public async Task<IActionResult> CreateJobPost([FromForm] CreateJobPostDto dto)
        {
            var command = CreateJobPostCommand.FromDto(dto);
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }

        [HttpPut("Update-Job-Post")]
        public async Task<IActionResult> UpdateJobPost([FromForm] EditJobPostDto dto)
        {
            var command = EditJobPostCommand.FromDto(dto);
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);

        }

        [HttpPut("Delete-My-Job-Post-By-Id/{id}")]
        public async Task<IActionResult> DeletejobPost(int id)
        {
            var result = await Mediator.Send(new DeletejobPostCommand (id));
            return NewResultStatusCode(result);
        }
    }
}
