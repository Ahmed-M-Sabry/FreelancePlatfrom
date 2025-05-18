using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.JopPostFeatrures.Queries.Models;
using FreelancePlatfrom.Core.Features.JopPostFeatrures.Queries.Result;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.JopPostFeatrures.Queries.Handler
{
    public class GetAllJobPostHandler : ResponseHandler, IRequestHandler<GetAllJobPostQuery, ApiResponse<List<GetAllJobPostDto>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IjobPostServices _jobPostService;

        public GetAllJobPostHandler(UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IjobPostServices jobPostServices)
        {
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _jobPostService = jobPostServices;
        }
        public async Task<ApiResponse<List<GetAllJobPostDto>>> Handle(GetAllJobPostQuery request, CancellationToken cancellationToken)
        {

            //var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            //if (string.IsNullOrEmpty(userId))
            //    return BadRequest<List<GetAllJobPostDto>>("User ID not found in token.");

            //// Verify user exists
            //var user = await _userManager.FindByIdAsync(userId);
            //if (user == null)
            //    return BadRequest<List<GetAllJobPostDto>>("User not found.");

            var jobPosts = await _jobPostService.GetAllJobPostsAsync();
            if (jobPosts == null || !jobPosts.Any())
                return NotFound<List<GetAllJobPostDto>>("No job posts found.");

            var jobPostsDto = _mapper.Map<List<GetAllJobPostDto>>(jobPosts);

            return Success<List<GetAllJobPostDto>>(jobPostsDto, new { TotalCount = jobPosts.Count() });
        }
    }
}
