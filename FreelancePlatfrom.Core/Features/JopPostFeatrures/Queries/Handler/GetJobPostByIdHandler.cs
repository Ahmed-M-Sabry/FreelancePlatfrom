using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Queries.Models;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Queries.Result;
using FreelancePlatfrom.Data.Entities.Identity;
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

namespace FreelancePlatfrom.Core.Features.jobPostFeatrures.Queries.Handler
{
    public class GetJobPostByIdHandler :ResponseHandler , IRequestHandler<GetJobPostByIdQuery, ApiResponse<GetJobPostByIdDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IjobPostServices _jobPostService;

        public GetJobPostByIdHandler(UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IjobPostServices jobPostServices)
        {
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _jobPostService = jobPostServices;
        }

        public async Task<ApiResponse<GetJobPostByIdDto>> Handle(GetJobPostByIdQuery request, CancellationToken cancellationToken)
        {
            // Get UserId from token
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<GetJobPostByIdDto>("User ID not found in token.");

            // Verify user exists
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<GetJobPostByIdDto> ("User not found.");

            var jobPost = await _jobPostService.GetByIdAsync(request.Id);
            if (jobPost == null)
                return NotFound<GetJobPostByIdDto> ("Job post not found.");

            var jobPostsDto = _mapper.Map<GetJobPostByIdDto>(jobPost);

            return Success<GetJobPostByIdDto>(jobPostsDto);
        }
    }

}
