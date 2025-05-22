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
    public class GetAcceptedMyApplyTaskHandler : ResponseHandler , IRequestHandler<GetAcceptedMyApplyTaskQuery, ApiResponse<List<GetAcceptedMyApplyTaskDto>>>
    {
        private readonly IApplyTaskService _applyTaskService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public GetAcceptedMyApplyTaskHandler(IApplyTaskService applyTaskService,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _applyTaskService = applyTaskService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<GetAcceptedMyApplyTaskDto>>> Handle(GetAcceptedMyApplyTaskQuery request, CancellationToken cancellationToken)
        {

            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
            if (userId is null)
                return Unauthorized<List<GetAcceptedMyApplyTaskDto>>();

            // Verify if the user is a freelancer
            var user = await _userManager.FindByIdAsync(userId);
            var isFreelancer = await _userManager.IsInRoleAsync(user, ApplicationRoles.Freelancer);
            if (!isFreelancer)
                return BadRequest<List<GetAcceptedMyApplyTaskDto>>("You Must Be a Freelancer");

            // Get all rejected apply tasks for the freelancer
            var acceptedAppltTasks = await _applyTaskService.GetAcceptedApplyTaskForFreelancer(userId);
            if (acceptedAppltTasks is null || acceptedAppltTasks.Count == 0)
                return NotFound<List<GetAcceptedMyApplyTaskDto>>("No Accepted Apply Tasks Found");

            // Map the rejected apply tasks to DTOs
            var acceptedAppltTasksDto = _mapper.Map<List<GetAcceptedMyApplyTaskDto>>(acceptedAppltTasks);
            if (acceptedAppltTasksDto is null)
                return BadRequest<List<GetAcceptedMyApplyTaskDto>>("Can't Map Rejected Apply Tasks to DTOs");

            return Success<List<GetAcceptedMyApplyTaskDto>>(acceptedAppltTasksDto, new { TotalCount = acceptedAppltTasksDto.Count });

        }
    }
}
