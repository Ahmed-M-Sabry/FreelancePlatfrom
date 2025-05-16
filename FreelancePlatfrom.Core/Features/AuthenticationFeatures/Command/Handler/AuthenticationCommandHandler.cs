    using FreelancePlatfrom.Core.Base;
    using FreelancePlatfrom.Core.Features.AuthenticationFeatures.Command.Model;
    using FreelancePlatfrom.Data.Entities.Identity;
    using FreelancePlatfrom.Data.Entities.Identity.Helper;
    using FreelancePlatfrom.Service.AbstractionServices;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    namespace FreelancePlatfrom.Core.Features.AuthenticationFeatures.Command.Handler
    {
        public class AuthenticationCommandHandler : ResponseHandler,
                    IRequestHandler<SiginInUserCommand, ApiResponse<ResponseAuthModel>>
        {
            private readonly IAuthenticatioService _authenticatioService;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public AuthenticationCommandHandler(IAuthenticatioService authenticatioService,
                UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                _authenticatioService = authenticatioService;
                _userManager = userManager;
                _roleManager = roleManager;
            }

        public async Task<ApiResponse<ResponseAuthModel>> Handle(SiginInUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .Include(u => u.refreshTokens)
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return BadRequest<ResponseAuthModel>("User not found.");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
                return BadRequest<ResponseAuthModel>("Invalid password.");

            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Any())
                return BadRequest<ResponseAuthModel>("User has no roles.");

            var authModel = await _authenticatioService.GenerateAuthModelAsync(user, request.RememberMeForMonth);
            return Success(authModel);
        }

    }
}