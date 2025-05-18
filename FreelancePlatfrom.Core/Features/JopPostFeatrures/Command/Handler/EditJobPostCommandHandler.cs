using AutoMapper;
using FluentValidation;
using FreelancePlatform.Service.AbstractionServices;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
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

namespace FreelancePlatfrom.Core.Features.jobPostFeatrures.Command.Handler
{
    public class EditJobPostCommandHandler : ResponseHandler, IRequestHandler<EditJobPostCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidator<EditJobPostCommand> _validator;
        private readonly ICategoryService _categoryService;
        private readonly IUserSkillesService _userSkillService;
        private readonly IMapper _mapper;
        private readonly IjobPostServices _jobPostService;
        private readonly IJobPostSkillServices _jobPostSkillServices;
        private readonly ISkillService _skillService;

        public EditJobPostCommandHandler(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IValidator<EditJobPostCommand> validator,
            ICategoryService categoryService,
            IUserSkillesService userSkillService,
            IMapper mapper,
            IjobPostServices jobPostService,
            IJobPostSkillServices jobPostSkillServices,
            ISkillService skillService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
            _categoryService = categoryService;
            _userSkillService = userSkillService;
            _mapper = mapper;
            _jobPostService = jobPostService;
            _jobPostSkillServices = jobPostSkillServices;
            _skillService = skillService;
        }
        public async Task<ApiResponse<string>> Handle(EditJobPostCommand request, CancellationToken cancellationToken)
        {
            // Validate the command
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ValidationFailed<string>(errors);
            }

            // Get UserId from token
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<string>("User ID not found in token.");

            // Verify user exists
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<string>("User not found.");

            // Verify JobPostId exists
            var jobPost = await _jobPostService.GetJobPostByIdAsync(userId ,request.Id);
            if (jobPost == null)
                return BadRequest<string>("Job post not found to Edit.");

            // Verify CategoryId exists
            var category = await _categoryService.GetByIdAsync(request.CategoryId);
            if (category == null)
                return BadRequest<string>("Invalid Category ID.");

            // Verify SkillIds if provided
            if (request.SkillIds != null && request.SkillIds.Any())
            {
            

                var validSkillIds = await _skillService.GetValidSkillIdsAsync(request.SkillIds);
                if (validSkillIds == null || validSkillIds.Count != request.SkillIds.Count)
                    return BadRequest<string>("Invalid SkillId(s)");

                var existingSkillIds = (await _jobPostSkillServices.GetSkillsByJobPostIdAsync(request.Id));

                var newSkillsToAdd = validSkillIds.Except(existingSkillIds).ToList();

                if (!newSkillsToAdd.Any())
                    return BadRequest<string>("All selected skills are already added.");


                await _jobPostSkillServices.RemoveSkillsByJobPostIdAsync(request.Id);

                var newJobPostSkills = request.SkillIds.Select(id => new JobPostSkill { JobPostId = request.Id, SkillId = id }).ToList();
                await _jobPostSkillServices.AddRangeAsync(newJobPostSkills);

            }
            // Set UserId in the request for mapping
            jobPost.Title = request.Title;
            jobPost.Description = request.Description;
            jobPost.Price = request.Price;
            jobPost.DurationTime = request.DurationTime;
            jobPost.CategoryId = request.CategoryId;
            jobPost.UserId = userId;


            // Map the command to the entity
            var jobPostEntity = _mapper.Map<JobPost>(jobPost);

            await _jobPostService.UpdateJobPostAsync(jobPostEntity);

            return  Success<string>("Job post Edit successfully");
        }
    }
}
