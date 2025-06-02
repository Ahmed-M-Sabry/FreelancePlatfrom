using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.FavoritesFreelancerFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.FavoritesFreelancerFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.JobPostFavoriteFeatrures.Queries.Models;
using FreelancePlatfrom.Data.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesFreelancerController : ApplicationControllerBase
    {
        [HttpGet("My-Favorites-Freelancer")]
        [Authorize(Roles = ApplicationRoles.User)]
        public async Task<IActionResult> GetFavorites()
        {
            var result = await Mediator.Send(new GetMyFavoritesFreelancerQuery());
            return NewResultStatusCode(result);
        }

        [HttpPost("Add-And-Remove-Favorite-Freelancer")]
        [Authorize(Roles = ApplicationRoles.User)]
        public async Task<IActionResult> AddAndRemoveFavoriteToFreelancer([FromForm] string FreelancerId)
        {
            var response = await Mediator.Send(new AddAndRemoveFavoriteFromFreelancerCommand(FreelancerId));
            return NewResultStatusCode(response);
        }

        [HttpPost("Add-Favorite-Freelancer")]
        [Authorize(Roles = ApplicationRoles.User)]
        public async Task<IActionResult> AddFavoriteToFreelancer([FromForm] string FreelancerId)
        {
            var response = await Mediator.Send(new AddFavoriteToFreelancerCommand(FreelancerId));
            return NewResultStatusCode(response);
        }

        [HttpDelete("Remove-Favorite-Freelancer")]
        [Authorize(Roles = ApplicationRoles.User)]
        public async Task<IActionResult> RemoveFavoriteToFreelancer([FromForm] string FreelancerId)
        {
            var response = await Mediator.Send(new RemoveFavoriteFromFreelancerCommand(FreelancerId));
            return NewResultStatusCode(response);
        }
    }
}
