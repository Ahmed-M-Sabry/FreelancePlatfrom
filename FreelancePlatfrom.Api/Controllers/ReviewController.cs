using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Queries.Models;
using FreelancePlatfrom.Data.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ApplicationRoles.User)]
    public class ReviewController : ApplicationControllerBase
    {
        [HttpPost("Create-Review")]
        public async Task<IActionResult> CreateReview([FromForm] CreateReviewCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResultStatusCode(response);
        }
        [HttpPut("Edit-Review")]
        public async Task<IActionResult> EditReview([FromForm] EditReviewCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResultStatusCode(response);
        }
        [HttpDelete("Delete-Review")]
        public async Task<IActionResult> DeleteReview([FromForm] int id)
        {
            var response = await Mediator.Send(new DeleteReviewCommand(id));
            return NewResultStatusCode(response);
        }
        [HttpGet("Get-Review-By-Id/{id}")]
        public async Task<IActionResult> GetRevieById([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetReviewByIdQuery(id));
            return NewResultStatusCode(response);
        }
        [HttpGet("Get-All-My-Reviewes")]
        public async Task<IActionResult> GetAllMyReviewes()
        {
            var response = await Mediator.Send(new GetAllMyReviewsQuery());
            return NewResultStatusCode(response);
        }
    }
}
