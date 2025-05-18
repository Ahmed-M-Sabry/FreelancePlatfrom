using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Queries.Result;
using FreelancePlatfrom.Core.Features.JopPostFeatrures.Command.Models;
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

namespace FreelancePlatfrom.Core.Features.JopPostFeatrures.Command.Handler
{
    public class DeletejobPostCommandHandler :ResponseHandler ,IRequestHandler<DeletejobPostCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IjobPostServices _jobPostService;

        public DeletejobPostCommandHandler(UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IjobPostServices jobPostServices)
        {
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _jobPostService = jobPostServices;
        }
        public async Task<ApiResponse<string>> Handle(DeletejobPostCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("uid")?.Value;

            if (string.IsNullOrEmpty(userId))
                return BadRequest<string>("User ID not found in token.");

            // Verify user exists
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<string>("User not found.");

            var jobPost = await _jobPostService.DeleteJobPost(userId ,request.Id);
            if (jobPost == null)
                return NotFound<string>("Job post not found to Delete.");

            return Deleted<string>("Job post deleted successfully.");
        }
    }
}
