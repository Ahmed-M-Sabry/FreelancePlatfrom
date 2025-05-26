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
    public class GetAllMyReportsHandler : ResponseHandler,
        IRequestHandler<GetAllMyReportsQuery, ApiResponse<List<GetReportResponse>>>
    {
        private readonly IReportService _reportService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetAllMyReportsHandler(IReportService reportService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _reportService = reportService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetReportResponse>>> Handle(GetAllMyReportsQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized<List<GetReportResponse>>();

            var reports = await _reportService.GetAllByClientIdAsync(userId);
            var mapped = _mapper.Map<List<GetReportResponse>>(reports);

            return Success(mapped);
        }
    }


}
