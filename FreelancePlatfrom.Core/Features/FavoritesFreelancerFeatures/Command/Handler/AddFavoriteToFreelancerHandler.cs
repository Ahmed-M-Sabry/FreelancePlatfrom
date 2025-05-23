using AutoMapper;
using FluentValidation;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.FavoritesFreelancerFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.JobPostFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.FavoritesTables;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Shared;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.FavoritesFreelancerFeatures.Command.Handler
{
    public class AddFavoriteToFreelancerHandler : ResponseHandler ,IRequestHandler<AddFavoriteToFreelancerCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFavoritesFreelancerServices _favoritesFreelancerServices;
        public AddFavoriteToFreelancerHandler( UserManager<ApplicationUser> userManager
            ,IHttpContextAccessor httpContextAccessor
            ,IFavoritesFreelancerServices favoritesFreelancerServices)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _favoritesFreelancerServices = favoritesFreelancerServices;
        }
        public async Task<ApiResponse<string>> Handle(AddFavoriteToFreelancerCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<string>("User ID not found in token.");

            // Verify user exists
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<string>("User not found.");

            // Is Client
            if (!await _userManager.IsInRoleAsync(user, ApplicationRoles.User))
                return BadRequest<string>("You Must Be A Client ");

            // Verify freelancer exists
            var freelancer = await _userManager.FindByIdAsync(request.UserId);
            if (freelancer == null)
                return BadRequest<string>("Freelancer not found.");

            // verify if the freelancer is already in Role
            if (! await _userManager.IsInRoleAsync(freelancer, ApplicationRoles.Freelancer))
                return BadRequest<string>("You Can't Only Favorite a Freelancer");

            // Verify if the freelancer is already in favorites
            var isFavorite = await _favoritesFreelancerServices.IsFreelancerFavorited(userId, request.UserId);
            if (isFavorite)
                return BadRequest<string>("Freelancer is already in favorites.");
            
            var favoritesFreelancer = new FavoritesFreelancer
            {
                ClientId = userId,
                FreelancerId = request.UserId
            };

            await _favoritesFreelancerServices.AddFavoriteToFreelancer(favoritesFreelancer);

            return Success<string>(request.UserId, new { Message = "Freelancer added to favorites." });
        }
    }
}
