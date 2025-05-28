using AutoMapper;
using Azure;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.JobPostFavoriteFeatrures.Queries.Models;
using FreelancePlatfrom.Core.Features.JobPostFavoriteFeatrures.Queries.Result;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.JobPostFavoriteFeatrures.Queries.Handler
{
    public class GetFavJobPostsHandler : ResponseHandler,
                IRequestHandler<GetFavJobPostsQuery, ApiResponse<List<FavJobPostResponse>>>

    {
        private readonly IFavoriteJobPostService _jobPostServices;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;


        public GetFavJobPostsHandler(IFavoriteJobPostService jobPostServices
            , IMapper mapper
            , IHttpContextAccessor httpContextAccessor
            , UserManager<ApplicationUser> userManager
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _jobPostServices = jobPostServices;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ApiResponse<List<FavJobPostResponse>>> Handle(GetFavJobPostsQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<List<FavJobPostResponse>>("User ID not found in token.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<List<FavJobPostResponse>>("User not found.");

            var favPosts = await _jobPostServices.GetFavoriteJobPostsAsync(userId);
            if(favPosts is null)
            {
                return NotFound<List<FavJobPostResponse>>("No Favorite Post");
            }
            return Success(_mapper.Map<List<FavJobPostResponse>>(favPosts), new { TotalCount = favPosts.Count() });
        }
    }

}
