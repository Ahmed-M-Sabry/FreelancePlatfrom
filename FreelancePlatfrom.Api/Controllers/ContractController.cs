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
    public class ContractController : ApplicationControllerBase
    {
        [HttpPost("Create-Contract")]
        [Authorize(Roles = ApplicationRoles.User)]
        public async Task<IActionResult> CreateNewContract([FromForm] CreateContractCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResultStatusCode(response);
        }
        [HttpPut("Edit-Contract")]
        [Authorize(Roles = ApplicationRoles.User)]
        public async Task<IActionResult> EditContract([FromForm] EditContractCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResultStatusCode(response);
        }
        [HttpDelete("Delete-Contract/{id}")]
        [Authorize(Roles = ApplicationRoles.User)]
        public async Task<IActionResult> DeleteContract([FromRoute] int id)
        {
            var response = await Mediator.Send(new DeleteContractCommand(id));
            return NewResultStatusCode(response);
        }
        [HttpPut("Accept-Contract/{id}")]
        [Authorize(Roles =ApplicationRoles.Freelancer)]
        public async Task<IActionResult> AcceptContract([FromRoute] int id)
        {
            var response = await Mediator.Send(new AcceptContractCommand(id));
            return NewResultStatusCode(response);
        }
        [HttpPut("Reject-Contract/{id}")]
        public async Task<IActionResult> RejectContract([FromRoute] int id)
        {
            var response = await Mediator.Send(new RejectContractCommand(id));
            return NewResultStatusCode(response);
        }



    }
}
