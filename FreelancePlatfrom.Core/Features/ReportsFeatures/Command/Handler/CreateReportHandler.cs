using AutoMapper;
using FluentValidation;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ReportsFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.Report;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ReportsFeatures.Command.Handler
{
    public class CreateReportHandler : ResponseHandler,
    IRequestHandler<CreateReportCommand, ApiResponse<string>>
    {
        private readonly IReportService _reportService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidator<CreateReportCommand> _validator;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateReportHandler(
            IReportService reportService,
            IHttpContextAccessor httpContextAccessor,
            IValidator<CreateReportCommand> validator,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _reportService = reportService;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ApiResponse<string>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate input
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return ValidationFailed<string>(validationResult.Errors.Select(e => e.ErrorMessage).ToList());

            // 2. Get user id from token
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized<string>();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound<string>("User not found");

            // 3. Map to entity
            var report = _mapper.Map<Reports>(request);
            report.ClientId = userId;
            report.ReportDate = DateTime.UtcNow;
            report.IsDeleted = false;

            // 4. Save to DB
            await _reportService.AddReportAsync(report);

            return Created<string>("Report created successfully");
        }
    }

}
