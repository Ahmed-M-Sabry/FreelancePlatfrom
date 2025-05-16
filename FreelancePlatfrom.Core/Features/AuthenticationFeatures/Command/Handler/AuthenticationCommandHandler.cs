using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.Command.Model;
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

namespace FreelancePlatfrom.Core.Features.AuthenticationFeatures.Command.Handler
{
    public class AuthenticationCommandHandler : ResponseHandler ,
                IRequestHandler<SiginInUserCommand, ApiResponse<string>>
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

        public async Task<ApiResponse<string>> Handle(SiginInUserCommand request, CancellationToken cancellationToken)
        {
            // Check if the user exists
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
                return BadRequest<string>("User not found.");

            // Check if the password is correct
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
                return BadRequest<string>("Invalid password.");

            var token = await _authenticatioService.CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Count == 0)
                return BadRequest<string>("User has no roles.");

            return Success<string>(user.Id, new {Token = token, Roles = roles.ToList() });
        }
    }
}
