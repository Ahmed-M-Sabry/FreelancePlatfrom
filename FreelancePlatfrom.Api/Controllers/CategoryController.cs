using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.CategoriesFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.CategoriesFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.SkillsFeatures.Queries.Models;
using FreelancePlatfrom.Data.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
        //[Authorize(Roles =ApplicationRoles.Admin)]
    public class CategoryController : ApplicationControllerBase
    {

        [HttpPost("Created-New-Category")]
        public async Task<IActionResult> CreateNewCategory([FromForm] string  categoryName)
        {
            var response = await Mediator.Send(new CreateCategoryCommand(categoryName));
            return NewResultStatusCode(response);
        }
        [HttpPut("Edit-Category")]
        public async Task<IActionResult> EditCategory([FromForm] EditCategoryCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResultStatusCode(response);
        }
        [HttpPut("Delete-Category")]
        public async Task<IActionResult> DeleteCategory([FromForm] int id)
        {
            var response = await Mediator.Send(new DeleteCategoryCommand(id));
            return NewResultStatusCode(response);
        }
        [HttpPut("Restore-Category")]
        public async Task<IActionResult> RestoreCategory([FromForm] int id)
        {
            var response = await Mediator.Send(new RestoreCategoryCommand(id));
            return NewResultStatusCode(response);
        }
        [HttpGet("Get-All-Category-For-Admin")]
        public async Task<IActionResult> GetAllCategoryForAdmin()
        {
            var response = await Mediator.Send(new GetAllSkillsForAdminQuery());
            return NewResultStatusCode(response);
        }
        [HttpGet("Get-All-Category-For-User")]
        public async Task<IActionResult> GetAllCategoryForUser()
        {
            var response = await Mediator.Send(new GetAllSkillsForUserQuery());
            return NewResultStatusCode(response);
        }
        [HttpGet("Get-Category-By-Id")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var response = await Mediator.Send(new GetCategoryByIdQuery(id));
            return NewResultStatusCode(response);
        }
        [HttpGet("Get-Category-By-Name")]
        public async Task<IActionResult> GetCategoryByName(string Name)
        {
            var response = await Mediator.Send(new GetCategoryByNameQuery(Name));
            return NewResultStatusCode(response);
        }
    }
}
