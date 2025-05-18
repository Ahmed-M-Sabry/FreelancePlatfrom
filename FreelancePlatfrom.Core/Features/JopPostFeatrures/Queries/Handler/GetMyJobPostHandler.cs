using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Queries.Models;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Queries.Result;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.jobPostFeatrures.Queries.Handler
{
    public class GetMyJobPostHandler : ResponseHandler , IRequestHandler<GetMyJobPostQuery, ApiResponse<List<GetMyJobPostDto>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IjobPostServices _jobPostService;

        public GetMyJobPostHandler(UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IjobPostServices jobPostServices)
        {
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _jobPostService = jobPostServices;
        }

        public async Task<ApiResponse<List<GetMyJobPostDto>>> Handle(GetMyJobPostQuery request, CancellationToken cancellationToken)
        {
            // Get UserId from token
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<List<GetMyJobPostDto>>("User ID not found in token.");

            // Verify user exists
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<List<GetMyJobPostDto>>("User not found.");

            var jobPosts = await _jobPostService.GetMyJobPostsAsync(userId);
            if (jobPosts == null || !jobPosts.Any())
                return NotFound<List<GetMyJobPostDto>>("No job posts found for this user.");

            var jobPostsDto = _mapper.Map<List<GetMyJobPostDto>>(jobPosts);

            return Success<List<GetMyJobPostDto>>(jobPostsDto, new { TotalCount = jobPosts.Count() });
        }
    }

}
