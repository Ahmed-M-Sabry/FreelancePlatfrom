using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ReportsFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.ReportsFeatures.Queries.Results;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.Report;
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

namespace FreelancePlatfrom.Core.Features.ReportsFeatures.Queries.Handler
{
    public class GetAllReportsForAdminHandler : ResponseHandler,
    IRequestHandler<GetAllReportsForAdminQuery, ApiResponse<List<GetReportResponse>>>
    {
        private readonly IReportService _reportService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;


        public GetAllReportsForAdminHandler(IReportService reportService
            , UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , IMapper mapper)
        {
            _reportService = reportService;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetReportResponse>>> Handle(GetAllReportsForAdminQuery request, CancellationToken cancellationToken)
        {
            //var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            //if (string.IsNullOrEmpty(userId))
            //    return BadRequest<List<Reports>>("User ID not found in token.");

            //// Verify user exists
            //var user = await _userManager.FindByIdAsync(userId);
            //if (user == null)
            //    return BadRequest<List<Reports>>("User not found.");

            //// Is Admin
            //if (!await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
            //    return BadRequest<List<Reports>>("You Must Be An Admin");
            var reports = await _reportService.GetAllAsync();

            var mapped = _mapper.Map<List<GetReportResponse>>(reports);

            return Success(mapped , new {TotalReports = mapped.Count() });
        }
    }

}
