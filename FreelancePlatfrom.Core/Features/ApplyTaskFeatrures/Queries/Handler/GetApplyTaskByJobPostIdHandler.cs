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
    public class GetApplyTaskByJobPostIdHandler : ResponseHandler , IRequestHandler<GetApplyTaskByJobPostIdQuery, ApiResponse<List<GetApplyTaskByJobPostIdDto>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplyTaskService _applyTaskService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public GetApplyTaskByJobPostIdHandler(UserManager<ApplicationUser> userManager,
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
        public async Task<ApiResponse<List<GetApplyTaskByJobPostIdDto>>> Handle(GetApplyTaskByJobPostIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
            if (userId is null)
                return Unauthorized<List<GetApplyTaskByJobPostIdDto>>();

            // Get All Apply Tasks
            var applyTasks = await _applyTaskService.GetAllApplyTask(userId);
            if (applyTasks is null)
                return BadRequest<List<GetApplyTaskByJobPostIdDto>>("Apply Tasks Not Found To Show");

            // Filter Apply Tasks by Job Post ID
            var filteredApplyTasks = applyTasks.Where(at => at.JobPostId == request.JobPostId).ToList();
            if (filteredApplyTasks.Count == 0)
                return BadRequest<List<GetApplyTaskByJobPostIdDto>>("No Apply Tasks Found for the Given Job Post ID");

            // Map Apply Tasks to DTOs
            var applyTaskDtos = _mapper.Map<List<GetApplyTaskByJobPostIdDto>>(filteredApplyTasks);
            if (applyTaskDtos == null)
                return BadRequest<List<GetApplyTaskByJobPostIdDto>>("Can't Map Apply Tasks to DTOs");

            // Success Response
            return Success<List<GetApplyTaskByJobPostIdDto>>(applyTaskDtos, new { TotalCount = applyTaskDtos.Count });
        }
    }
}
