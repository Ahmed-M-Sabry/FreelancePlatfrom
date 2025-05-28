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
    public class EditFreelancerProfessionalInfoHandler : ResponseHandler, IRequestHandler<EditFreelancerProfessionalInfoCommand, ApiResponse<string>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditFreelancerProfessionalInfoHandler(IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<ApiResponse<string>> Handle(EditFreelancerProfessionalInfoCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
            if (userId == null) return Unauthorized<string>();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound<string>("User not found");

            user.YourTitle = request.YourTitle ?? user.YourTitle;
            user.Description = request.Description ?? user.Description;
            user.Education = request.Education ?? user.Education;
            user.Experience = request.Experience ?? user.Experience;
            user.HourlyRate = request.HourlyRate ?? user.HourlyRate;

            await _userManager.UpdateAsync(user);
            return  Created("Professional info updated successfully");
        }
    }
}
