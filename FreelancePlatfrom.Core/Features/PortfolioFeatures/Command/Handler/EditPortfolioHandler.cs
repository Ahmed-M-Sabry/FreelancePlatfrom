using AutoMapper;
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
    public class EditPortfolioHandler : ResponseHandler,
    IRequestHandler<EditPortfolioCommand, ApiResponse<string>>
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public EditPortfolioHandler(IPortfolioService portfolioService, IHttpContextAccessor httpContextAccessor
            , IFileService fileService
            ,IMapper mapper)
        {
            _portfolioService = portfolioService;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(EditPortfolioCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized<string>();

            var existing = await _portfolioService.GetByIdAsync(request.Id);
            if (existing == null || existing.UserId != userId)
                return NotFound<string>("Portfolio not found or not owned by you.");

            _mapper.Map(request, existing);

            if (request.Media != null)
            {
                try
                {
                    var mediaPath = await _fileService.UploadFileAsync2(request.Media);
                    existing.Media = mediaPath;
                }
                catch (Exception ex)
                {
                    return BadRequest<string>($"File upload failed: {ex.Message}");
                }
            }

            await _portfolioService.UpdateAsync(existing);
            return Success("Portfolio updated successfully");
        }

    }

}
