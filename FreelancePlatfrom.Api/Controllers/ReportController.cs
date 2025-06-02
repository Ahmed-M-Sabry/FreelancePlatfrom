using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.ReportsFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.ReportsFeatures.Queries.Models;
using FreelancePlatfrom.Data.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ApplicationRoles.User + "," + ApplicationRoles.Freelancer)]
    public class ReportController : ApplicationControllerBase
    {
        [HttpGet("Get-All-My-Reports")]
        public async Task<IActionResult> GetAllMyReports()
        {
            var response = await Mediator.Send(new GetAllMyReportsQuery());
            return NewResultStatusCode(response);
        }

        [HttpGet("Get-All-Reports-For-Admin")]
        // [Authorize(Roles = ApplicationRoles.Admin)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllReportsForAdmin()
        {
            var response = await Mediator.Send(new GetAllReportsForAdminQuery());
            return NewResultStatusCode(response);
        }

        [HttpGet("Get-Report-By-Id/{id}")]
        public async Task<IActionResult> GetReportById([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetReportByIdQuery(id));
            return NewResultStatusCode(response);
        }

        [HttpPost("Create-Report")]
        public async Task<IActionResult> CreateReport([FromForm] CreateReportCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResultStatusCode(response);
        }

        [HttpPut("Edit-Report")]
        public async Task<IActionResult> EditReport([FromForm] EditReportCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResultStatusCode(response);
        }

        [HttpDelete("Delete-Report")]
        public async Task<IActionResult> DeleteReport([FromForm] int id)
        {
            var response = await Mediator.Send(new DeleteReportCommand(id));
            return NewResultStatusCode(response);
        }
    }
}
