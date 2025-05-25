using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.CountryFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.CountryFeatures.Queries.Models;
using FreelancePlatfrom.Data.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(ApplicationRoles.Admin)]
    public class CountryController : ApplicationControllerBase
    {
        [HttpPost("Create-Country")]
        public async Task<IActionResult> CreateCountry([FromForm] CreateCountryCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }
        [HttpPut("Edit-Country")]
        public async Task<IActionResult> EditCountry([FromForm] EditCountryCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResultStatusCode(result);
        }
        [HttpPut("Delete-Country")]
        public async Task<IActionResult> DeleteCountry([FromForm] string id)
        {
            var result = await Mediator.Send(new DeleteCountryCommand(id));
            return NewResultStatusCode(result);
        }
        [HttpPut("Restor-Country")]
        public async Task<IActionResult> RestorCountry([FromForm] string id)
        {
            var result = await Mediator.Send(new RestorCountryCommand(id));
            return NewResultStatusCode(result);
        }
        [HttpGet("Get-All-Country-For-Admin")]
        public async Task<IActionResult> GetAllCountryForAdmin()
        {
            var result = await Mediator.Send(new GetAllCountryForAdminQuery());
            return NewResultStatusCode(result);
        }
        [HttpGet("Get-All-Country-For-User")]
        public async Task<IActionResult> GetAllCountryForUser()
        {
            var result = await Mediator.Send(new GetAllCountryForUserQuery());
            return NewResultStatusCode(result);
        }
        [HttpGet("Country-By-Id")]
        public async Task<IActionResult> CountryById(string id)
        {
            var result = await Mediator.Send(new GetCountryByIdQuery(id));
            return NewResultStatusCode(result);
        }
        [HttpGet("Country-By-Name")]
        public async Task<IActionResult> CountryByName(string id)
        {
            var result = await Mediator.Send(new GetCountryByNameQuery(id));
            return NewResultStatusCode(result);
        }
    }
}
