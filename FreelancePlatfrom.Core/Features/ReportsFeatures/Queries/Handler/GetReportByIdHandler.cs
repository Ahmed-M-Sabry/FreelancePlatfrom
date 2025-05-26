using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ReportsFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.ReportsFeatures.Queries.Results;
using FreelancePlatfrom.Data.Entities.Report;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ReportsFeatures.Queries.Handler
{
    public class GetReportByIdHandler : ResponseHandler,
    IRequestHandler<GetReportByIdQuery, ApiResponse<GetReportResponse>>
    {
        private readonly IReportService _reportService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetReportByIdHandler(IReportService reportService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _reportService = reportService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetReportResponse>> Handle(GetReportByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized<GetReportResponse>();

            var report = await _reportService.GetReportByIdAsync(request.Id);
            if (report == null || report.ClientId != userId || report.IsDeleted)
                return NotFound<GetReportResponse>("Report not found or unauthorized access");
            var mapped = _mapper.Map<GetReportResponse>(report);
            return Success(mapped);
        }
    }


}
