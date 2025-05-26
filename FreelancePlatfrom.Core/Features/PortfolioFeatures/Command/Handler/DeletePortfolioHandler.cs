using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.PortfolioFeatures.Command.Models;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.PortfolioFeatures.Command.Handler
{
    public class DeletePortfolioHandler : ResponseHandler,
    IRequestHandler<DeletePortfolioCommand, ApiResponse<string>>
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeletePortfolioHandler(IPortfolioService portfolioService, IHttpContextAccessor httpContextAccessor)
        {
            _portfolioService = portfolioService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<string>> Handle(DeletePortfolioCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized<string>();

            var portfolio = await _portfolioService.GetByIdAsync(request.Id);
            if (portfolio == null || portfolio.UserId != userId)
                return NotFound<string>("Portfolio not found or not owned by you.");

            await _portfolioService.DeleteAsync(portfolio);
            return Deleted<string>("Portfolio deleted successfully");
        }
    }

}
