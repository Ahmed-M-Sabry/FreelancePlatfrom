using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Models;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Result;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
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

namespace FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Handler
{
    public class GetRejectedMyApplyTaskHandler : ResponseHandler ,IRequestHandler<GetRejectedMyApplyTaskQuery, ApiResponse<List<GetRejectedMyApplyTaskDto>>>
    {
        private readonly IApplyTaskService _applyTaskService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public GetRejectedMyApplyTaskHandler(IApplyTaskService applyTaskService,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _applyTaskService = applyTaskService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<GetRejectedMyApplyTaskDto>>> Handle(GetRejectedMyApplyTaskQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
            if (userId is null)
                return Unauthorized<List<GetRejectedMyApplyTaskDto>>();

            // Verify if the user is a freelancer
            var user = await _userManager.FindByIdAsync(userId);
            var isFreelancer = await _userManager.IsInRoleAsync(user, ApplicationRoles.Freelancer);
            if(!isFreelancer)
                return BadRequest<List<GetRejectedMyApplyTaskDto>>("You Must Be a Freelancer");

            // Get all rejected apply tasks for the freelancer
            var rejectedApplyTasks = await _applyTaskService.GetRejectedApplyTaskForFreelancer(userId);
            if (rejectedApplyTasks is null || rejectedApplyTasks.Count == 0)
                return NotFound<List<GetRejectedMyApplyTaskDto>>("No Rejected Apply Tasks Found");

            // Map the rejected apply tasks to DTOs
            var rejectedApplyTaskDtos = _mapper.Map<List<GetRejectedMyApplyTaskDto>>(rejectedApplyTasks);
            if (rejectedApplyTaskDtos is null)
                return BadRequest<List<GetRejectedMyApplyTaskDto>>("Can't Map Rejected Apply Tasks to DTOs");

            return Success<List<GetRejectedMyApplyTaskDto>>(rejectedApplyTaskDtos, new { TotalCount = rejectedApplyTaskDtos.Count });

        }
    }
}
