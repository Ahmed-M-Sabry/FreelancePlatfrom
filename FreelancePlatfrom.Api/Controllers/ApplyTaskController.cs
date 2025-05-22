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
        [HttpGet("Get-My-All-ApplyTask-Freelancer")]
        [Authorize(Roles =ApplicationRoles.Freelancer)]
        public async Task<IActionResult> GetMyAllApplyTaskFreelancer()
        {
            var result = await Mediator.Send(new GetFreelancerApplyTaskQuery());
            return NewResultStatusCode(result);
        }
        [HttpGet("Get-My-All-ApplyTask-Client")]
        [Authorize(Roles = ApplicationRoles.User)]
        public async Task<IActionResult> GetMyAllApplyTaskClient()
        {
            var result = await Mediator.Send(new GetClientApplyTaskQurey());
            return NewResultStatusCode(result);
        }
        [HttpGet("Get-ApplyTask-By-JobPost-Id")]
        public async Task<IActionResult> GetMyAllApplyTaskByJobPostID(int id)
        {
            var result = await Mediator.Send(new GetApplyTaskByJobPostIdQuery(id));
            return NewResultStatusCode(result);
        }
        [HttpGet("Get-ApplyTask-By-Id")]
        public async Task<IActionResult> GetMyAllApplyTaskID(int id)
        {
            var result = await Mediator.Send(new GetApplyTaskByIdQuery(id));
            return NewResultStatusCode(result);
        }
        [HttpGet("Get-Accepted-My-ApplyTask")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> GetAcceptedMyApplyTask()
        {
            var result = await Mediator.Send(new GetAcceptedMyApplyTaskQuery());
            return NewResultStatusCode(result);
        }
        [HttpGet("Get-Rejected-My-ApplyTask")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> GetRejectedMyApplyTask()
        {
            var result = await Mediator.Send(new GetRejectedMyApplyTaskQuery());
            return NewResultStatusCode(result);
        }
        [HttpGet("Get-Pending-My-ApplyTask")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> GetPendingMyApplyTask()
        {
            var result = await Mediator.Send(new GetPendingMyApplyTaskQuery());
            return NewResultStatusCode(result);
        }
        [HttpPost("Apply-In-New-JobPost")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> ApplyInNewJobPost([FromForm] CreateApplyTaskDto createApplyTaskDto)
        {
            var command = CreateApplyTaskCommand.FromDto(createApplyTaskDto);
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }
        [HttpPut("Accept-ApplyTask")]
        [Authorize(Roles = ApplicationRoles.User)]
        public async Task<IActionResult> AcceptApplyTask([FromForm] int id)
        {
            var result = await Mediator.Send(new AcceptApplyTaskCommand(id));
            return NewResultStatusCode(result);
        }
        [HttpPut("Reject-ApplyTask")]
        [Authorize(Roles = ApplicationRoles.User)]
        public async Task<IActionResult> RejectApplyTask([FromForm] int id)
        {
            var result = await Mediator.Send(new RejectApplyTaskCommand(id));
            return NewResultStatusCode(result);
        }
        [HttpPut("Update-ApplyTask")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> UpdateApplyTask([FromForm] EditApplyTaskDto editApplyTaskDto)
        {
            var command = EditApplyTaskCommand.FromDto(editApplyTaskDto);
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }

        [HttpDelete("Delete-ApplyTask-By-Id")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> DeleteApplyTask([FromForm] int id)
        {
            var result = await Mediator.Send(new DeleteApplyTaskCommand(id));
            return NewResultStatusCode(result);
        }

    }
}
