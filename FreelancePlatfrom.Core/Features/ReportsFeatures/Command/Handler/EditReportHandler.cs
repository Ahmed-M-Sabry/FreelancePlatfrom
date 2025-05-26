using AutoMapper;
using FluentValidation;
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
    public class EditReportHandler : ResponseHandler,
    IRequestHandler<EditReportCommand, ApiResponse<string>>
    {
        private readonly IReportService _reportService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidator<EditReportCommand> _validator;
        private readonly IMapper _mapper;

        public EditReportHandler(
            IReportService reportService,
            IHttpContextAccessor httpContextAccessor,
            IValidator<EditReportCommand> validator,
            IMapper mapper)
        {
            _reportService = reportService;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(EditReportCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return ValidationFailed<string>(validationResult.Errors.Select(x => x.ErrorMessage).ToList());

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized<string>();

            var existingReport = await _reportService.GetReportByIdAsync(request.Id);
            if (existingReport == null || existingReport.ClientId != userId)
                return NotFound<string>("Report not found or you are not authorized to edit this report");

            existingReport.Type = request.Type;
            existingReport.Description = request.Description;

            await _reportService.UpdateReportAsync(existingReport);

            return Success("Report updated successfully");
        }
    }

}
