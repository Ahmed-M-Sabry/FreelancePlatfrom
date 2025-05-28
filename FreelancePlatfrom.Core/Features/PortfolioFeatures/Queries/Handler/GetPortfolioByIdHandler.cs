using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.PortfolioFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.PortfolioFeatures.Queries.Response;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;

namespace FreelancePlatfrom.Core.Features.PortfolioFeatures.Queries.Handler
{
    public class GetPortfolioByIdHandler : ResponseHandler,
        IRequestHandler<GetPortfolioByIdQuery, ApiResponse<PortfolioByIdResponse>>
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IMapper _mapper;

        public GetPortfolioByIdHandler(IPortfolioService portfolioService, IMapper mapper)
        {
            _portfolioService = portfolioService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<PortfolioByIdResponse>> Handle(GetPortfolioByIdQuery request, CancellationToken cancellationToken)
        {
            var portfolio = await _portfolioService.GetByIdAsync(request.Id);

            if (portfolio == null)
                return NotFound<PortfolioByIdResponse>("Portfolio not found");

            var response = _mapper.Map<PortfolioByIdResponse>(portfolio);
            return Success(response);
        }
    }
}
