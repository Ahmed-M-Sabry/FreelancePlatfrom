using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.UserSkillFreature.Command.Model;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.UserSkillFreature.Command.Handler
{
    public class DeleteUserSkillHandler :ResponseHandler ,IRequestHandler<DeleteUserSkillByIdCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserSkillesService _skillService;
        public DeleteUserSkillHandler(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IUserSkillesService skillService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _skillService = skillService;
        }
        public async Task<ApiResponse<string>> Handle(DeleteUserSkillByIdCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<string>("User ID not found in token.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<string>("User not found.");

            var SkillRuslt = await _skillService.GetUserSkillIdsAsync(userId);
            if (SkillRuslt == null || SkillRuslt.Count == 0)
                return BadRequest<string>("No skills found for the user.");

            if (!SkillRuslt.Contains(request.SkillId))
                return BadRequest<string>("Skill not found.");

            await _skillService.RemoveUserSkillById(userId, request.SkillId);

            return Deleted<string>("Skill deleted successfully.");

        }

    }
}
