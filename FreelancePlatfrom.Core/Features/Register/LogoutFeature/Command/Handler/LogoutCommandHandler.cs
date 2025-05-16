using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.Register.LogoutFeature.Command.Model;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.Register.LogoutFeature.Command.Handler
{
    public class LogoutCommandHandler : ResponseHandler , IRequestHandler<LogoutCommand, ApiResponse<string>>

    {
        private readonly IAuthenticatioService _authService;

        public LogoutCommandHandler(IAuthenticatioService authService)
        {
            _authService = authService;
        }

        public async Task<ApiResponse<string>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var result = await _authService.RevokeRefreshTokenFromCookiesAsync();

            if (result)
                return Success("Logout successful.");

            return BadRequest<string>("Logout failed or token already revoked.");

        }
    }

}
