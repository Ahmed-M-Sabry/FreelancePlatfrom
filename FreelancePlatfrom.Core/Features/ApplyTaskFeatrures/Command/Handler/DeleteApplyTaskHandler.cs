using AutoMapper;
using FluentValidation;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Models;
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

namespace FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Handler
{
    public class DeleteApplyTaskHandler : ResponseHandler, IRequestHandler<DeleteApplyTaskCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplyTaskService _applyTaskService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeleteApplyTaskHandler(UserManager<ApplicationUser> userManager,
            IApplyTaskService applyTaskService,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _userManager = userManager;
            _applyTaskService = applyTaskService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResponse<string>> Handle(DeleteApplyTaskCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
            if (userId is null)
                return Unauthorized<string>();

            // Verify if the user is a freelancer
            var user = await _userManager.FindByIdAsync(userId);
            var isFreelancer = await _userManager.IsInRoleAsync(user, ApplicationRoles.Freelancer);
            if (!isFreelancer)
                return BadRequest<string>("You Must Be a Freelancer to Delete Your Job Post");

            // Verify if the job post exists
            var existingApplyTask = await _applyTaskService.GetApplyTask(userId, request.Id);
            if (existingApplyTask is null)
                return BadRequest<string>("Apply Task Not Found To Delete");

            var DeletedApplyTask = await _applyTaskService.DeleteApplyTask(existingApplyTask);
            if (DeletedApplyTask == null)
                return BadRequest<string>("Can't Delete Apply Task");

            return Success<string>("Apply Task Deleted Successfully");
        }
    }
}
