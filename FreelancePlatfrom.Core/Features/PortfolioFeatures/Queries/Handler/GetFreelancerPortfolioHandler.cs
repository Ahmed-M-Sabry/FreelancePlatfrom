using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.PortfolioFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.PortfolioFeatures.Queries.Response;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.PortfolioFeatures.Queries.Handler
{
    public class GetFreelancerPortfolioHandler : ResponseHandler,
        IRequestHandler<GetFreelancerPortfolioQuery, ApiResponse<List<FreelancerPortfolioResponse>>>
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IMapper _mapper;

        public GetFreelancerPortfolioHandler(IPortfolioService portfolioService, IMapper mapper)
        {
            _portfolioService = portfolioService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<FreelancerPortfolioResponse>>> Handle(GetFreelancerPortfolioQuery request, CancellationToken cancellationToken)
        {
            var portfolios = await _portfolioService.GetByFreelancerIdAsync(request.FreelancerId);

            if (portfolios == null || portfolios.Count == 0)
                return NotFound<List<FreelancerPortfolioResponse>>("No portfolio found for this freelancer.");

            var response = _mapper.Map<List<FreelancerPortfolioResponse>>(portfolios);

            return Success(response);
        }

    }
}
