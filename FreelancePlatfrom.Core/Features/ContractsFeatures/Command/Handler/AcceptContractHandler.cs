using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Shared;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Handler
{
    public class AcceptContractHandler : ResponseHandler,
        IRequestHandler<AcceptContractCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContractService _contractService;

        public AcceptContractHandler(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IContractService contractService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _contractService = contractService;
        }

        public async Task<ApiResponse<string>> Handle(AcceptContractCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<string>("User not authenticated");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<string>("User not found");

            if (!await _userManager.IsInRoleAsync(user, ApplicationRoles.Freelancer))
                return BadRequest<string>("Only freelancers can accept contracts");

            var contract = await _contractService.GetContractById(request.Id);
            if (contract == null)
                return NotFound<string>("Contract not found");

            if (contract.FreelancerId != userId)
                return BadRequest<string>("You are not authorized to accept this contract");

            if (contract.Status == ContractStatus.Accepted)
                return BadRequest<string>("Contract is already accepted");

            contract.Status = ContractStatus.Accepted;
            await _contractService.UpdateContract(contract);

            return Success<string>("Contract accepted successfully", contract.Id.ToString());
        }
    }
}
