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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Handler
{
    public class DeleteContractHandler : ResponseHandler,
       IRequestHandler<DeleteContractCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContractService _contractService;

        public DeleteContractHandler(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IContractService contractService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _contractService = contractService;
        }

        public async Task<ApiResponse<string>> Handle(DeleteContractCommand request, CancellationToken cancellationToken)
        {
            // Get User ID from token
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<string>("User not authenticated");

            // Get contract
            var contract = await _contractService.GetContractById(request.Id);
            if (contract == null)
                return NotFound<string>("Contract not found");

            // Check ownership
            if (contract.ClientId != userId)
                return BadRequest<string>("You are not authorized to delete this contract");

            // Check if freelancer already accepted
            if (contract.Status == ContractStatus.Accepted)
                return BadRequest<string>("Cannot delete a contract that has already been accepted");

            // Proceed to delete
            await _contractService.DeleteContract(contract);

            return Success<string>("Contract deleted successfully", contract.Id.ToString());
        }
    } 
}
