using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.CategoriesFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Models;
using FreelancePlatfrom.Data.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles =ApplicationRoles.User)]
    public class ContractController : ApplicationControllerBase
    {
        [HttpPost("Create-Contract")]
        public async Task<IActionResult> CreateNewContract([FromForm] CreateContractCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResultStatusCode(response);
        }
    }
}
