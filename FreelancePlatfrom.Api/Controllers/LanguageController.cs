using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.CategoriesFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.LanguageFratures.Command.Models;
using FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Models;
using FreelancePlatfrom.Core.Features.LanguagesFeatures.Query.GetAllLanguagesForAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = ApplicationRoles.Admin)]
    public class LanguageController : ApplicationControllerBase
    {

        [HttpPost("Create-New-Language")]
        public async Task<IActionResult> CreateNewLanguage([FromForm] CreateLanguageCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResultStatusCode(response);
        }
        [HttpPut("Edit-Language")]
        public async Task<IActionResult> EditLanguage([FromForm] EditLanguageCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResultStatusCode(response);
        }
        [HttpPut("Delete-Language")]
        public async Task<IActionResult> DeleteLanguage([FromForm] DeleteLanguageCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResultStatusCode(response);
        }
        [HttpPut("Restore-Language")]
        public async Task<IActionResult> RestoreLanguage([FromForm] RestoreLanguageCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResultStatusCode(response);
        }
        [HttpGet("Get-All-Language-For-Admin")]
        public async Task<IActionResult> GetAllLanguage()
        {
            var response = await Mediator.Send(new GetAllLanguagesForAdminQuery());
            return NewResultStatusCode(response);
        }
        [HttpGet("Get-All-Language-For-User")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllLanguageForUser()
        {
            var response = await Mediator.Send(new GetAllLanguagesForUserQuery());
            return NewResultStatusCode(response);
        }
        [HttpGet("Get-Language-By-Id")]
        public async Task<IActionResult> GetLanguageById(string id)
        {
            var response = await Mediator.Send(new GetLanguageByIdQuery(id));
            return NewResultStatusCode(response);
        }
        [HttpGet("Get-Language-By-Name")]
        public async Task<IActionResult> GetLanguageByName(string value)
        {
            var response = await Mediator.Send(new GetLanguageByNameQuery(value));
            return NewResultStatusCode(response);
        }
    }
}
