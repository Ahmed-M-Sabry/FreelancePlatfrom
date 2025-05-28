using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.AccountFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AccountFeatures.Command.Handler
{
    public class EditFreelancerLocationHandler : ResponseHandler, IRequestHandler<EditFreelancerLocationCommand, ApiResponse<string>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditFreelancerLocationHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<ApiResponse<string>> Handle(EditFreelancerLocationCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
            if (userId == null) return Unauthorized<string>();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound<string>("User not found");

            user.State = request.State ?? user.State;
            user.Address = request.Address ?? user.Address;
            user.CountryId = request.CountryId ?? user.CountryId;
            user.Zip = request.Zip ?? user.Zip;

            await _userManager.UpdateAsync(user);
            return Success("Location info updated successfully");
        }
    }
}
