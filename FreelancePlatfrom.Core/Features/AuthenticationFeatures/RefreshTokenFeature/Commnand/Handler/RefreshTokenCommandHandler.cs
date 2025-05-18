using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.RefreshTokenFeature.Commnand.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.Identity.Helper;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AuthenticationFeatures.RefreshTokenFeature.Commnand.Handler
{
    public class RefreshTokenCommandHandler : ResponseHandler, IRequestHandler<RefreshTokenCommand, ApiResponse<ResponseAuthModel>>
    {
        private readonly IAuthenticatioService _authenticatioService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RefreshTokenCommandHandler(IAuthenticatioService authenticatioService, IHttpContextAccessor httpContextAccessor)
        {
            _authenticatioService = authenticatioService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<ResponseAuthModel>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["RefreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return BadRequest<ResponseAuthModel>("No refresh token provided.");

            var result = await _authenticatioService.RefreshTokenAsunc(refreshToken);

            if (!string.IsNullOrEmpty(result.Message))
                return BadRequest<ResponseAuthModel>(result.Message);

            return Success(result);
        }
    }
}
