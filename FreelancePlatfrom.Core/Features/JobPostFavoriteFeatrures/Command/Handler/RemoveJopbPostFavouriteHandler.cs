using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.JobPostFavoriteFeatrures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.Data.Shared;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.Service.AbstractionServices;
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
    public class RemoveJopbPostFavouriteHandler : ResponseHandler, IRequestHandler<RemoveJopbPostFavouriteCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFavoriteJobPostService _favoriteJobPostService;
        private readonly IjobPostServices _jobPostServices;
        public RemoveJopbPostFavouriteHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , IFavoriteJobPostService favoriteJobPostService
            , IjobPostServices jobPostServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _favoriteJobPostService = favoriteJobPostService;
            _jobPostServices = jobPostServices;
        }
        public async Task<ApiResponse<string>> Handle(RemoveJopbPostFavouriteCommand request, CancellationToken cancellationToken)
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
            var isJobPostFavorite = await _favoriteJobPostService.IsJobPostFavorited(userId, request.JobPostId);
            if(isJobPostFavorite == null)
                return BadRequest<string>("JobPost is not in favorites.");

            await _favoriteJobPostService.RemoveJobPostFavourite(isJobPostFavorite);
            return Deleted<string>("JobPost removed from favorites successfully.");

        }

    }
}
