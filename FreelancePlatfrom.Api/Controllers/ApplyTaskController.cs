using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Dtos;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Models;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Models;
using FreelancePlatfrom.Data.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplyTaskController : ApplicationControllerBase
    {
        // GET Methods - Freelancer
        [HttpGet("GetAllApplyTasksFreelancer")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> GetAllApplyTasksFreelancer()
        {
            var result = await Mediator.Send(new GetFreelancerApplyTaskQuery());
            return NewResultStatusCode(result);
        }

        [HttpGet("GetAcceptedApplyTasksFreelancer")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> GetAcceptedApplyTasksFreelancer()
        {
            var result = await Mediator.Send(new GetAcceptedMyApplyTaskQuery());
            return NewResultStatusCode(result);
        }

        [HttpGet("GetRejectedApplyTasksFreelancer")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> GetRejectedApplyTasksFreelancer()
        {
            var result = await Mediator.Send(new GetRejectedMyApplyTaskQuery());
            return NewResultStatusCode(result);
        }

        [HttpGet("GetPendingApplyTasksFreelancer")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> GetPendingApplyTasksFreelancer()
        {
            var result = await Mediator.Send(new GetPendingMyApplyTaskQuery());
            return NewResultStatusCode(result);
        }

        // GET Methods - Client
        [HttpGet("GetAllApplyTasksClient")]
        [Authorize(Roles = ApplicationRoles.User)]
        public async Task<IActionResult> GetAllApplyTasksClient()
        {
            var result = await Mediator.Send(new GetClientApplyTaskQurey());
            return NewResultStatusCode(result);
        }

        // GET Methods - General
        [HttpGet("GetApplyTaskByJobPostId")]
        public async Task<IActionResult> GetApplyTaskByJobPostId(int id)
        {
            var result = await Mediator.Send(new GetApplyTaskByJobPostIdQuery(id));
            return NewResultStatusCode(result);
        }

        [HttpGet("GetApplyTaskById")]
        public async Task<IActionResult> GetApplyTaskById(int id)
        {
            var result = await Mediator.Send(new GetApplyTaskByIdQuery(id));
            return NewResultStatusCode(result);
        }

        // POST Methods
        [HttpPost("ApplyForJobPost")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> ApplyForJobPost([FromForm] CreateApplyTaskDto createApplyTaskDto)
        {
            var command = CreateApplyTaskCommand.FromDto(createApplyTaskDto);
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }

        // PUT Methods
        [HttpPut("AcceptApplyTask")]
        [Authorize(Roles = ApplicationRoles.User)]
        public async Task<IActionResult> AcceptApplyTask([FromForm] int id)
        {
            var result = await Mediator.Send(new AcceptApplyTaskCommand(id));
            return NewResultStatusCode(result);
        }

        [HttpPut("RejectApplyTask")]
        [Authorize(Roles = ApplicationRoles.User)]
        public async Task<IActionResult> RejectApplyTask([FromForm] int id)
        {
            var result = await Mediator.Send(new RejectApplyTaskCommand(id));
            return NewResultStatusCode(result);
        }

        [HttpPut("UpdateApplyTask")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> UpdateApplyTask([FromForm] EditApplyTaskDto editApplyTaskDto)
        {
            var command = EditApplyTaskCommand.FromDto(editApplyTaskDto);
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }

        // DELETE Methods
        [HttpDelete("DeleteApplyTaskById")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> DeleteApplyTaskById([FromForm] int id)
        {
            var result = await Mediator.Send(new DeleteApplyTaskCommand(id));
            return NewResultStatusCode(result);
        }
    }
}