using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.PortfolioFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.PortfolioFeatures.Queries.Response;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.PortfolioFeatures.Queries.Handler
{
    public class GetMyPortfoliosHandler : ResponseHandler,
        IRequestHandler<GetMyPortfoliosQuery, ApiResponse<List<MyPortfoliosResponse>>>
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetMyPortfoliosHandler(IPortfolioService portfolioService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _portfolioService = portfolioService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<List<MyPortfoliosResponse>>> Handle(GetMyPortfoliosQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized<List<MyPortfoliosResponse>>();

            var portfolios = await _portfolioService.GetByFreelancerIdAsync(userId);

            if (portfolios == null || !portfolios.Any())
                return NotFound<List<MyPortfoliosResponse>>("You have no portfolios.");

            var response = _mapper.Map<List<MyPortfoliosResponse>>(portfolios);

            return Success(response);
        }
    }
}
