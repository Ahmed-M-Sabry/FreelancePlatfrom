using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.PortfolioFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.PortfolioFeatures.Queries.Models;
using FreelancePlatfrom.Data.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ApplicationControllerBase
    {
        [HttpGet("Freelancer-Portofolio/{freelancerId}")]
        [Authorize]
        public async Task<IActionResult> GetFreelancerPortfolio(string freelancerId)
        {
            var result = await Mediator.Send(new GetFreelancerPortfolioQuery(freelancerId));
            return NewResultStatusCode(result);
        }

        [HttpGet("My-Portofolios")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> GetMyPortfolios()
        {
            var result = await Mediator.Send(new GetMyPortfoliosQuery());
            return NewResultStatusCode(result);
        }

        [HttpGet("Portofolio-Get-By-Id/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await Mediator.Send(new GetPortfolioByIdQuery(id));
            return NewResultStatusCode(result);
        }

        [HttpPost("Create-Portofolio")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> CreatePortfolio([FromForm] CreatePortfolioCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }

        [HttpPut("Edit-Portofolio")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> EditPortfolio([FromForm] EditPortfolioCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }

        [HttpDelete("Delete-Portofolio/{id}")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> DeletePortfolio([FromRoute] int id)
        {
            var result = await Mediator.Send(new DeletePortfolioCommand(id));
            return NewResultStatusCode(result);
        }
    }
}
