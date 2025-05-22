using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Models;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Result;
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

namespace FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Handler
{
    public class GetPendingMyApplyTaskHandler : ResponseHandler, IRequestHandler<GetPendingMyApplyTaskQuery, ApiResponse<List<GetPendingMyApplyTaskDto>>>
    {
        private readonly IApplyTaskService _applyTaskService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public GetPendingMyApplyTaskHandler(IApplyTaskService applyTaskService,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _applyTaskService = applyTaskService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<GetPendingMyApplyTaskDto>>> Handle(GetPendingMyApplyTaskQuery request, CancellationToken cancellationToken)
        {

            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
            if (userId is null)
                return Unauthorized<List<GetPendingMyApplyTaskDto>>();

            // Verify if the user is a freelancer
            var user = await _userManager.FindByIdAsync(userId);
            var isFreelancer = await _userManager.IsInRoleAsync(user, ApplicationRoles.Freelancer);
            if (!isFreelancer)
                return BadRequest<List<GetPendingMyApplyTaskDto>>("You must be a freelancer to view pending apply tasks.");

            // Get Only Pending Apply Tasks
            var applyTasks = await _applyTaskService.GetAllApplyTask(userId);
            if (applyTasks is null)
                return BadRequest<List<GetPendingMyApplyTaskDto>>("No apply tasks found.");

            // Filter only pending ones (assuming there's a Status property)
            var pendingApplyTasks = await _applyTaskService.GetPendingApplyTaskForFreelancer(userId);
            if (pendingApplyTasks is null)
                return BadRequest<List<GetPendingMyApplyTaskDto>>("No pending apply tasks found.");

            // Map Apply Tasks to Pending DTOs
            var applyTaskDtos = _mapper.Map<List<GetPendingMyApplyTaskDto>>(pendingApplyTasks);
            if (applyTaskDtos == null)
                return BadRequest<List<GetPendingMyApplyTaskDto>>("Can't map apply tasks to DTOs.");

            // Success Response
            return Success<List<GetPendingMyApplyTaskDto>>(applyTaskDtos, new { TotalCount = applyTaskDtos.Count });

        }
    }
}
