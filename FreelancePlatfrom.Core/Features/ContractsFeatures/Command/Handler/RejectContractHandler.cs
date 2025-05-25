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
    public class RejectContractHandler : ResponseHandler,
        IRequestHandler<RejectContractCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContractService _contractService;

        public RejectContractHandler(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IContractService contractService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _contractService = contractService;
        }

        public async Task<ApiResponse<string>> Handle(RejectContractCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<string>("User not authenticated");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<string>("User not found");

            if (!await _userManager.IsInRoleAsync(user, ApplicationRoles.Freelancer))
                return BadRequest<string>("Only freelancers can reject contracts");

            var contract = await _contractService.GetContractById(request.ContractId);
            if (contract == null)
                return NotFound<string>("Contract not found");

            if (contract.FreelancerId != userId)
                return BadRequest<string>("You are not authorized to reject this contract");

            if (contract.Status != ContractStatus.Pending)
                return BadRequest<string>("Only pending contracts can be rejected");

            contract.Status = ContractStatus.Rejected;
            await _contractService.UpdateContract(contract);

            return Success<string>("Contract rejected successfully", contract.Id.ToString());
        }
    }
}
