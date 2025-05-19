using AutoMapper;
using FluentValidation;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Models;
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
    public class GetFreelancerApplyTaskHandler : ResponseHandler , IRequestHandler<GetFreelancerApplyTaskQuery, ApiResponse<List<GetFreelancerApplyTaskDto>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplyTaskService _applyTaskService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetFreelancerApplyTaskHandler(UserManager<ApplicationUser> userManager,
            IApplyTaskService applyTaskService,
            IHttpContextAccessor httpContextAccessor,
            IjobPostServices jobPostService,
            IMapper mapper
            )
        {
            _userManager = userManager;
            _applyTaskService = applyTaskService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<GetFreelancerApplyTaskDto>>> Handle(GetFreelancerApplyTaskQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
            if (userId is null)
                return Unauthorized<List<GetFreelancerApplyTaskDto>>();

            // Verify if the user is a freelancer
            var user = await _userManager.FindByIdAsync(userId);
            var isFreelancer = await _userManager.IsInRoleAsync(user, ApplicationRoles.Freelancer);
            if (!isFreelancer)
                return BadRequest<List<GetFreelancerApplyTaskDto>>("You Must Be a Freelancer to Delete Your Job Post");

            // Get All Apply Tasks
            var applyTasks = await _applyTaskService.GetAllApplyTask(userId);
            if (applyTasks is null)
                return BadRequest<List<GetFreelancerApplyTaskDto>>("Apply Tasks Not Found To Show");

            // Map Apply Tasks to DTOs
            var applyTaskDtos = _mapper.Map<List<GetFreelancerApplyTaskDto>>(applyTasks);
            if (applyTaskDtos == null)
                return BadRequest<List<GetFreelancerApplyTaskDto>>("Can't Map Apply Tasks to DTOs");

            // Success Response
            return Success<List<GetFreelancerApplyTaskDto>>(applyTaskDtos, new { TotalCount = applyTaskDtos.Count });

        }
    }

}
