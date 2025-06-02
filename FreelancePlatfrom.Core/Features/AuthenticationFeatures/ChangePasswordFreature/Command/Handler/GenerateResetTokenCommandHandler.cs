using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.ChangePasswordFreature.Command.Model;
using FreelancePlatfrom.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AuthenticationFeatures.ChangePasswordFreature.Command.Handler
{
    public class GenerateResetTokenCommandHandler
    : ResponseHandler, IRequestHandler<GenerateResetTokenCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GenerateResetTokenCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApiResponse<string>> Handle(GenerateResetTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return BadRequest<string>("User not found.");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return Success(token, "Token generated successfully.");
        }
    }

}
