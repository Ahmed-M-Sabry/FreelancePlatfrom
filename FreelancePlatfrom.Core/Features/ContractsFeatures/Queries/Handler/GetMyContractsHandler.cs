using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Queries.Results;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ContractsFeatures.Queries.Handler
{
    public class GetMyContractsHandler : ResponseHandler,
    IRequestHandler<GetMyContractsQuery, ApiResponse<List<GetMyContractsResponse>>>
    {
        private readonly IContractService _contractService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetMyContractsHandler(
            IContractService contractService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _contractService = contractService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetMyContractsResponse>>> Handle(GetMyContractsQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized<List<GetMyContractsResponse>>();

            var contracts = await _contractService.GetMyContractsAsync(userId);
            if(contracts.Count() == 0)
            {
                return NotFound<List<GetMyContractsResponse>>("Not Any Contract Found");
            }
            var response = _mapper.Map<List<GetMyContractsResponse>>(contracts);

            return Success(response , new {TotalNumberOfContracts = contracts.Count()});
        }
    }

}
