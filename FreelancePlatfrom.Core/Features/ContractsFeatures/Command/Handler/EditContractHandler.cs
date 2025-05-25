using AutoMapper;
using FluentValidation;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Shared;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Handler
{
    public class EditContractHandler : ResponseHandler
        , IRequestHandler<EditContractCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidator<EditContractCommand> _validator;
        private readonly ICategoryService _categoryService;
        private readonly IUserSkillesService _userSkillService;
        private readonly IMapper _mapper;
        private readonly IjobPostServices _jobPostService;
        private readonly ISkillService _skillService;
        private readonly IContractService _contractService;
        private readonly IApplyTaskService _applyTaskService;

        public EditContractHandler(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IValidator<EditContractCommand> validator,
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

        public async Task<ApiResponse<string>> Handle(EditContractCommand request, CancellationToken cancellationToken)
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

            var contract = await _contractService.GetContractById(request.Id);
            if (contract == null)
                return NotFound<string>("Contract not found");

            if (contract.ClientId != userId)
                return BadRequest<string>("You are not authorized to edit this contract");

            if (contract.Status == ContractStatus.Accepted)
                return BadRequest<string>("Cannot edit a contract that has already been accepted");

            var newContract =  _mapper.Map(request, contract);

            await _contractService.UpdateContract(newContract);

            return Created<string>("Contract updated successfully", contract.Id.ToString());
        }
    }
}
