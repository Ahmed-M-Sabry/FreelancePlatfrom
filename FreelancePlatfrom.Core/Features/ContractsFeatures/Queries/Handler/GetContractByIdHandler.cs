using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Queries.Results;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ContractsFeatures.Queries.Handler
{
    public class GetContractByIdHandler : ResponseHandler,
                IRequestHandler<GetContractByIdQuery, ApiResponse<ContractDetailsResponse>>
    {
        private readonly IContractService _contractService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;



        public GetContractByIdHandler(IContractService contractService, IMapper mapper
            , IHttpContextAccessor httpContextAccessor
            ,UserManager<ApplicationUser> userManager)
        {
            _contractService = contractService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<ApiResponse<ContractDetailsResponse>> Handle(GetContractByIdQuery request, CancellationToken cancellationToken)
        {
            // Get UserId from token
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<ContractDetailsResponse>("User ID not found in token.");

            // Verify user exists
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<ContractDetailsResponse>("User not found.");

            var contract = await _contractService.GetContractWithIncludes(request.ContractId , userId);

            if (contract == null)
                return NotFound<ContractDetailsResponse>("Contract not found");

            var response = _mapper.Map<ContractDetailsResponse>(contract);

            return Success(response);
        }
    }

}
