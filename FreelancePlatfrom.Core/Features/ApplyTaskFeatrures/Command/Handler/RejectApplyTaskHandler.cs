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
    public class RejectApplyTaskHandler : ResponseHandler ,IRequestHandler<RejectApplyTaskCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplyTaskService _applyTaskService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IjobPostServices _jobPostService;
        public RejectApplyTaskHandler(UserManager<ApplicationUser> userManager,
            IApplyTaskService applyTaskService,
            IHttpContextAccessor httpContextAccessor,
            IjobPostServices jobPostService
            )
        {
            _userManager = userManager;
            _applyTaskService = applyTaskService;
            _httpContextAccessor = httpContextAccessor;
            _jobPostService = jobPostService;

        }
        public async Task<ApiResponse<string>> Handle(RejectApplyTaskCommand request, CancellationToken cancellationToken)
        {

            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
            if (userId is null)
                return Unauthorized<string>();

            // Verify if the user is a freelancer
            var user = await _userManager.FindByIdAsync(userId);
            var isClient = await _userManager.IsInRoleAsync(user, ApplicationRoles.User);
            if (!isClient)
                return BadRequest<string>("You Must Be a Client to Reject This Apply Task");

            // Verify if the Apply Task exists
            var existingApplyTask = await _applyTaskService.GetApplyTask(userId, request.Id);
            if (existingApplyTask is null)
                return BadRequest<string>("Apply Task Not Found To Reject");

            // Verify if the Apply Task is already accepted
            if (existingApplyTask.Status == ApplyTaskStatus.Accepted)
                return BadRequest<string>("Apply Task Already Accepted");

            // Verify if the Apply Task is already rejected
            if (existingApplyTask.Status == ApplyTaskStatus.Rejected)
                return BadRequest<string>("Apply Task Already Rejected");

            // Accept the Apply Task
            var acceptApplyTast = await _applyTaskService.RejectApplyTask(userId, request.Id);
            if (acceptApplyTast.Status != ApplyTaskStatus.Rejected)
                return BadRequest<string>("Some thing Error Happen Try Again");

            return Success<string>("Apply Task Rejected Successfully");
        }
    }
}
