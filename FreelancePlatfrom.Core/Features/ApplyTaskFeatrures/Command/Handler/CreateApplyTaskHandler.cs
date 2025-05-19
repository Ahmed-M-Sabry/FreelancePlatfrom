using AutoMapper;
using FluentValidation;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Models;
using FreelancePlatfrom.Core.Features.JobPostFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.Data.Shared;
using FreelancePlatfrom.Service.AbstractionServices;
using FreelancePlatfrom.Service.ImplementationServices;
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
    public class CreateApplyTaskHandler : ResponseHandler, IRequestHandler<CreateApplyTaskCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplyTaskService _applyTaskService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IjobPostServices _jobPostService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateApplyTaskCommand> _validator;
        public CreateApplyTaskHandler(UserManager<ApplicationUser> userManager,
            IApplyTaskService applyTaskService,
            IHttpContextAccessor httpContextAccessor,
            IjobPostServices jobPostService,
            IMapper mapper,
            IValidator<CreateApplyTaskCommand> validator
            )
        {
            _userManager = userManager;
            _applyTaskService = applyTaskService;
            _httpContextAccessor = httpContextAccessor;
            _jobPostService = jobPostService;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<ApiResponse<string>> Handle(CreateApplyTaskCommand request, CancellationToken cancellationToken)
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
            if(userId is null)
                return Unauthorized<string>();

            // Verify if the user is a freelancer
            var user = await _userManager.FindByIdAsync(userId);
            var isFreelancer = await _userManager.IsInRoleAsync(user, ApplicationRoles.Freelancer);
            if(!isFreelancer)
                return BadRequest<string>("You Must Be a Freelancer to Apply This Job Post");

            // Verify if the job post exists
            var jobPost = await _jobPostService.GetByIdAndNotDeletedAsync(request.JobPostId);
            if (jobPost is null)
                return BadRequest<string>("Job Post Not Found To Apply");

            // Verify if the user has already applied for the job post
            var existingApplication = await _applyTaskService.GetByJobPostIdAndFreelancerIdAsync(request.JobPostId, userId);
            if (existingApplication != null)
                return BadRequest<string>("You Have Already Applied For This Job Post");

            // Client Id is Exits
            var Client = await _userManager.FindByIdAsync(request.ClientId);
            if(Client is null)
                return BadRequest<string>("Client Not Found");

            // Verify if the client is the owner of the job post
            if (jobPost.UserId != request.ClientId)
                return BadRequest<string>("Client Not the Owner of This Job Post");

            //Mapping the command to the entity
            request.FreelancerId = userId;
            var applyTask = _mapper.Map<ApplyTask>(request);
            if(applyTask is null)
                return BadRequest<string>("Failed to Mapping Apply Task");

            var applyTaskCreate = await _applyTaskService.AddAsync(applyTask);

            if (applyTaskCreate is null)
                return BadRequest<string>("Failed to Add Apply Task");

            return Created<string>("Apply Task Created Successfully");

        }
    }
}
