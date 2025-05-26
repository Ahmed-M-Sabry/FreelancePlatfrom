using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ReportsFeatures.Command.Models;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ReportsFeatures.Command.Handler
{
    public class DeleteReportHandler : ResponseHandler,
    IRequestHandler<DeleteReportCommand, ApiResponse<string>>
    {
        private readonly IReportService _reportService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteReportHandler(IReportService reportService, IHttpContextAccessor httpContextAccessor)
        {
            _reportService = reportService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<string>> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized<string>();

            var report = await _reportService.GetReportByIdAsync(request.Id);
            if (report == null || report.ClientId != userId)
                return NotFound<string>("Report not found or unauthorized access");

            await _reportService.DeleteReportAsync(report);

            return Deleted<string>("Report deleted successfully");
        }
    }

}
