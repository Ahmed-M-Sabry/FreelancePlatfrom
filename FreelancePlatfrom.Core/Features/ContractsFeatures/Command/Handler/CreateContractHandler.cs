using AutoMapper;
using FluentValidation;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Validator;
using FreelancePlatfrom.Core.Features.JobPostFeatures.Command.Models;
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

namespace FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Handler
{
    public class CreateContractHandler : ResponseHandler
        , IRequestHandler<CreateContractCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidator<CreateContractCommand> _validator;
        private readonly ICategoryService _categoryService;
        private readonly IUserSkillesService _userSkillService;
        private readonly IMapper _mapper;
        private readonly IjobPostServices _jobPostService;
        private readonly ISkillService _skillService;
        private readonly IContractService _contractService;
        private readonly IApplyTaskService _applyTaskService;

        public CreateContractHandler(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IValidator<CreateContractCommand> validator,
            ICategoryService categoryService,
            IUserSkillesService userSkillService,
            IjobPostServices jobPostService,
            IMapper mapper,
            ISkillService skillService,
            IContractService contractService,
            IApplyTaskService applyTaskService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
            _categoryService = categoryService;
            _userSkillService = userSkillService;
            _mapper = mapper;
            _jobPostService = jobPostService;
            _skillService = skillService;
            _contractService = contractService;
            _applyTaskService = applyTaskService;
        }
        public async Task<ApiResponse<string>> Handle(CreateContractCommand request, CancellationToken cancellationToken)
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

            // Is Client
            if (!await _userManager.IsInRoleAsync(user, ApplicationRoles.User))
                return BadRequest<string>("You Must Be A Client ");

            // Verify freelancer exists
            var freelancer = await _userManager.FindByIdAsync(request.FreelancerId);
            if (freelancer == null)
                return BadRequest<string>("Freelancer not found.");

            // verify if the freelancer is already in Role
            if (!await _userManager.IsInRoleAsync(freelancer, ApplicationRoles.Freelancer))
                return BadRequest<string>("You Can only Make a Contract With a Freelancer");

            // ApplyTask is Exist And Completed
            var applyTask = await _applyTaskService.GetApplyTaskBetweenClientAndFreelancer(userId, request.FreelancerId, request.ApplyTaskId);
            if (applyTask == null)
                return NotFound<string>("We Can't Found Any Apply Task With You and Freelancer");

            if (applyTask.Status != ApplyTaskStatus.Accepted)
                return BadRequest<string>("You Must Accepted Apply Task Before Make Contract");
            
            var contract = _mapper.Map<Contracts>(request);
            contract.ClientId = userId;

            await _contractService.AddContract(contract);

            return Created<string>("Contract Created , You Can Edit Or Delete It Before Freelancer Accept");
        }
    }
}
