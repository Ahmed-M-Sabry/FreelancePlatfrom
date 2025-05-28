using FreelancePlatfrom.Api.ApplicationBase;
using FreelancePlatfrom.Core.Features.FavoritesFreelancerFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.JobPostFavoriteFeatrures.Command.Models;
using FreelancePlatfrom.Core.Features.JobPostFavoriteFeatrures.Queries.Models;
using FreelancePlatfrom.Core.Features.JopPostFeatrures.Queries.Models;
using FreelancePlatfrom.Data.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreelancePlatfrom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesJobPostController : ApplicationControllerBase
    {
        [HttpPost("Add-Favourite-JobPost")]
        [Authorize(Roles =ApplicationRoles.Freelancer)]
        public async Task<IActionResult> AddFavoriteToJobPost([FromForm] int jobPostId)
        {
            var response = await Mediator.Send(new AddJopbPostFavouriteCommand(jobPostId));
            return NewResultStatusCode(response);
        }
        [HttpDelete("Delete-Favourite-JobPost")]
        [Authorize(Roles =ApplicationRoles.Freelancer)]
        public async Task<IActionResult> RemoveFavouriteFromJobPost([FromForm] int jobPostId)
        {
            var response = await Mediator.Send(new RemoveJopbPostFavouriteCommand(jobPostId));
            return NewResultStatusCode(response);
        }
        [HttpPost("Add-Delete-Favourite-JobPost")]
        [Authorize(Roles = ApplicationRoles.Freelancer)]
        public async Task<IActionResult> AddandRemoveFavouriteFromJobPost([FromForm] int jobPostId)
        {
            var response = await Mediator.Send(new AddAndRmoveJopbPostFavouriteCommand(jobPostId));
            return NewResultStatusCode(response);
        }

        [Authorize(Roles = ApplicationRoles.Freelancer)]
        [HttpGet("My-Favorites-JobPost")]
        public async Task<IActionResult> GetFavorites()
        {
            var result = await Mediator.Send(new GetFavJobPostsQuery());
            return NewResultStatusCode(result);
        }

    }
}
