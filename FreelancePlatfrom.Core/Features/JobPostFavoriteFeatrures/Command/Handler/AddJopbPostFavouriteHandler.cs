using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.JobPostFavoriteFeatrures.Command.Models;
using FreelancePlatfrom.Data.Entities.FavoritesTables;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.Data.Shared;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.Service.AbstractionServices;
using FreelancePlatfrom.Service.ImplementationServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.JobPostFavoriteFeatrures.Command.Handler
{
    internal class AddJopbPostFavouriteHandler : ResponseHandler, IRequestHandler<AddJopbPostFavouriteCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFavoriteJobPostService _favoriteJobPostService;
        private readonly IjobPostServices _jobPostServices;
        public AddJopbPostFavouriteHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , IFavoriteJobPostService favoriteJobPostService
            , IjobPostServices jobPostServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _favoriteJobPostService = favoriteJobPostService;
            _jobPostServices = jobPostServices;
        }
        public async Task<ApiResponse<string>> Handle(AddJopbPostFavouriteCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<string>("User ID not found in token.");

            // Verify user exists
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<string>("User not found.");

            // Is Freelancer
            if (!await _userManager.IsInRoleAsync(user, ApplicationRoles.Freelancer))
                return BadRequest<string>("You Must Be A Freelancer ");

            // Verify jobPost exists
            JobPost jobPostIsExist = await _jobPostServices.GetByIdAsync(request.JobPostId);
            if (jobPostIsExist == null)
                return BadRequest<string>("JobPost not found.");

            // Verify if the jobPost is already in favorites
            var isJobPostFavorite = await _favoriteJobPostService.IsJobPostFavorited(userId , request.JobPostId);

            if(isJobPostFavorite != null)
                return BadRequest<string>("JobPost already favorited.");

            var favoriteJobPost = new FavJobPost
            {
                FreelancerId = userId,
                JobPostId = request.JobPostId
            };
            await _favoriteJobPostService.AddJobPostFavourite(favoriteJobPost);
            return Created<string>("JobPost favorited successfully.");
        }
    }
}
