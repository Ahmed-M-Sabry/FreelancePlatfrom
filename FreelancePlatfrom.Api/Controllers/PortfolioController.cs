using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.PortfolioFeatures.Command.Models;
using FreelancePlatfrom.Data.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = ApplicationRoles.Freelancer)]
    public class PortfolioController : ApplicationControllerBase
    {
        [HttpPost("Create-Portofolio")]
        public async Task<IActionResult> CreatePortfolio([FromForm] CreatePortfolioCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }
        [HttpPut("Edit-Portofolio")]
        public async Task<IActionResult> EditPortfolio([FromForm] EditPortfolioCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }
        [HttpDelete("Delete-Portofolio/{id}")]
        public async Task<IActionResult> DeletePortfolio([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeletePortfolioCommand(id));
            return NewResultStatusCode(result);
        }
    }

}
