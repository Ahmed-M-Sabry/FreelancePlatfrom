using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.AccountFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using FreelancePlatfrom.Service.AbstractionServices;
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
    public class EditFreelancerSkillsCommandHandler : ResponseHandler, IRequestHandler<EditFreelancerSkillsCommand, ApiResponse<string>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISkillService _skillService;
        private readonly IUserSkillesService _userSkillsService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditFreelancerSkillsCommandHandler(
            IHttpContextAccessor httpContextAccessor,
            ISkillService skillService,
            IUserSkillesService userSkillsService,
            UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _skillService = skillService;
            _userSkillsService = userSkillsService;
            _userManager = userManager;
        }

        public async Task<ApiResponse<string>> Handle(EditFreelancerSkillsCommand request, CancellationToken cancellationToken)
        {
            // Get UserId from token
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<string>("User ID not found in token.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<string>("User not found.");

            // Validate Skill IDs
            if (request.SkillIds == null || !request.SkillIds.Any())
                return BadRequest<string>("At least one skill must be selected.");

            var validSkillIds = await _skillService.GetValidSkillIdsAsync(request.SkillIds);
            if (validSkillIds == null || validSkillIds.Count != request.SkillIds.Count)
                return BadRequest<string>("One or more Skill IDs are invalid.");

            // Get current skills
            var existingSkillIds = await _userSkillsService.GetUserSkillIdsAsync(userId);

            // Determine skills to add and remove
            var skillsToAdd = validSkillIds.Except(existingSkillIds).ToList();
            var skillsToRemove = existingSkillIds.Except(validSkillIds).ToList();

            // Apply removals
            if (skillsToRemove.Any())
            {
                await _userSkillsService.RemoveUserSkillsAsync(userId, skillsToRemove);
            }

            // Apply additions
            if (skillsToAdd.Any())
            {
                var newUserSkills = skillsToAdd.Select(skillId => new UserSkill
                {
                    UserId = userId,
                    SkillId = skillId
                }).ToList();

                await _userSkillsService.AddUserSkillsAsync(newUserSkills);
            }

            return Success<string>("Freelancer skills updated successfully.");
        }
    }
}
