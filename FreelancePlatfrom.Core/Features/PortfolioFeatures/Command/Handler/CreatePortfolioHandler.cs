//using AutoMapper;
//using FreelancePlatfrom.Core.Base;
//using FreelancePlatfrom.Core.Features.PortfolioFeatures.Command.Models;
//using FreelancePlatfrom.Data.Entities;
//using FreelancePlatfrom.Service.AbstractionServices;
//using MediatR;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FreelancePlatfrom.Core.Features.PortfolioFeatures.Command.Handler
//{
//    public class CreatePortfolioHandler : ResponseHandler,
//    IRequestHandler<CreatePortfolioCommand, ApiResponse<string>>
//    {
//        private readonly IPortfolioService _portfolioService;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly IFileService _fileService;
//        private readonly IMapper _mapper;

//        public CreatePortfolioHandler(
//            IPortfolioService portfolioService,
//            IHttpContextAccessor httpContextAccessor,
//            IFileService fileService,
//            IMapper mapper)
//        {
//            _portfolioService = portfolioService;
//            _httpContextAccessor = httpContextAccessor;
//            _fileService = fileService;
//            _mapper = mapper;
//        }

//        public async Task<ApiResponse<string>> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
//        {
//            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
//            if (string.IsNullOrEmpty(userId))
//                return Unauthorized<string>();

//            var portfolio = _mapper.Map<Portfolio>(request);
//            portfolio.UserId = userId;

//            if (request.Media != null)
//            {
//                try
//                {
//                    var mediaPath = await _fileService.UploadFileAsync2(request.Media);
//                    portfolio.Media = mediaPath;
//                }
//                catch (Exception ex)
//                {
//                    return BadRequest<string>($"File upload failed: {ex.Message}");
//                }
//            }

//            await _portfolioService.AddAsync(portfolio);
//            return Created<string>("Portfolio created successfully");
//        }
//    }
//}

using AutoMapper;
using FreelancePlatform.Service.AbstractionServices;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.PortfolioFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace FreelancePlatform.Core.Features.PortfolioFeatures.Command.Handler
{
    public class CreatePortfolioHandler : ResponseHandler, IRequestHandler<CreatePortfolioCommand, ApiResponse<string>>
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePortfolioHandler> _logger;

        public CreatePortfolioHandler(
            IPortfolioService portfolioService,
            IHttpContextAccessor httpContextAccessor,
            IFileService fileService,
            IMapper mapper,
            ILogger<CreatePortfolioHandler> logger)
        {
            _portfolioService = portfolioService;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ApiResponse<string>> Handle(CreatePortfolioCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling CreatePortfolioCommand");

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("User ID not found in token");
                return Unauthorized<string>();
            }

            var portfolio = _mapper.Map<Portfolio>(request);
            portfolio.UserId = userId;

            if (request.Media != null)
            {
                try
                {
                    _logger.LogInformation("Uploading media file");
                    var mediaPath = await _fileService.UploadFileAsync2(request.Media);
                    portfolio.Media = mediaPath;
                    _logger.LogInformation("Media uploaded successfully: {MediaPath}", mediaPath);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to upload media file");
                    return BadRequest<string>($"File upload failed: {ex.Message}");
                }
            }

            await _portfolioService.AddAsync(portfolio);
            _logger.LogInformation("Portfolio created successfully for user: {UserId}", userId);
            return Created<string>("Portfolio created successfully");
        }
    }
}