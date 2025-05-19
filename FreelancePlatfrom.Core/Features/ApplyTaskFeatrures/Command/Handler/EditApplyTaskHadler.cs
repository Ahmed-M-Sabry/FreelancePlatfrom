using AutoMapper;
using FluentValidation;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Models;
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

namespace FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Handler
{
    public class EditApplyTaskHadler : ResponseHandler , IRequestHandler<EditApplyTaskCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplyTaskService _applyTaskService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IjobPostServices _jobPostService;
        private readonly IMapper _mapper;
        private readonly IValidator<EditApplyTaskCommand> _validator;
        public EditApplyTaskHadler(UserManager<ApplicationUser> userManager,
            IApplyTaskService applyTaskService,
            IHttpContextAccessor httpContextAccessor,
            IjobPostServices jobPostService,
            IMapper mapper,
            IValidator<EditApplyTaskCommand> validator
            )
        {
            _userManager = userManager;
            _applyTaskService = applyTaskService;
            _httpContextAccessor = httpContextAccessor;
            _jobPostService = jobPostService;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<ApiResponse<string>> Handle(EditApplyTaskCommand request, CancellationToken cancellationToken)
        {

            // Validate the command
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ValidationFailed<string>(errors);
            }

            // Verify if the user is authenticated
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
            if (userId is null)
                return Unauthorized<string>();

            // Verify if the user is a freelancer
            var user = await _userManager.FindByIdAsync(userId);
            var isFreelancer = await _userManager.IsInRoleAsync(user, ApplicationRoles.Freelancer);
            if (!isFreelancer)
                return BadRequest<string>("You Must Be a Freelancer to Edit This Job Post");

            // Verify if the job post exists
            var existingApplyTask = await _applyTaskService.GetApplyTask(userId , request.Id);
            if (existingApplyTask is null)
                return BadRequest<string>("Apply Task Not Found To Edit");

            // Map the command to the ApplyTask entity
            existingApplyTask.FreelancerId = userId;
            existingApplyTask.OfferDescription = request.OfferDescription;
            existingApplyTask.TotalAmount = request.TotalAmount;
            existingApplyTask.DeliveryDate = request.DeliveryDate;

            var applyTask = _mapper.Map<ApplyTask>(existingApplyTask);
            if(applyTask is null)
                return BadRequest<string>("Apply Task Can't Mapping");

            // Update the ApplyTask entity
            var result = await _applyTaskService.UpdateAsync(applyTask);
            if (result is null)
                return BadRequest<string>("Failed to Update Apply Task");
            
            return Success<string>("Apply Task Updated Successfully");

        }
    }

}
