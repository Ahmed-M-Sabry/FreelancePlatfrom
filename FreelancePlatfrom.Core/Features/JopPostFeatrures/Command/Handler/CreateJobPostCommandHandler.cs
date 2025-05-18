using AutoMapper;
using FluentValidation;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.JobPostFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.infrastructure.Data;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace FreelancePlatform.Core.Features.JobPostFeatures.Command.Handlers
{
    public class CreateJobPostCommandHandler : ResponseHandler, IRequestHandler<CreateJobPostCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidator<CreateJobPostCommand> _validator;
        private readonly ICategoryService _categoryService;
        private readonly IUserSkillesService _userSkillService;
        private readonly IMapper _mapper;
        private readonly IjobPostServices _jobPostService;
        private readonly ISkillService _skillService;

        public CreateJobPostCommandHandler(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IValidator<CreateJobPostCommand> validator,
            ICategoryService categoryService,
            IUserSkillesService userSkillService,
            IjobPostServices jobPostService,
            IMapper mapper,
            ISkillService skillService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
            _categoryService = categoryService;
            _userSkillService = userSkillService;
            _mapper = mapper;
            _jobPostService = jobPostService;
            _skillService = skillService;
        }

        public async Task<ApiResponse<string>> Handle(CreateJobPostCommand request, CancellationToken cancellationToken)
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

            // Verify CategoryId exists
            var category = await _categoryService.GetByIdAsync(request.CategoryId);
            if (category == null)
                return BadRequest<string>("Invalid Category ID.");

            // Verify SkillIds if provided
            if (request.SkillIds != null && request.SkillIds.Any())
            {
                var validSkillIds = await _skillService.GetValidSkillIdsAsync(request.SkillIds);
                if (validSkillIds == null || validSkillIds.Count != request.SkillIds.Count)
                    return BadRequest<string>("Invalid Skill IDs.");
            }

            // Set UserId in the request for mapping
            request.UserId = userId;

            // Map command to JobPost entity
            var jobPost = _mapper.Map<JobPost>(request);

            await _jobPostService.AddJobPost(jobPost);

            return Created<string>("Job post created successfully.");
        }
    }
}