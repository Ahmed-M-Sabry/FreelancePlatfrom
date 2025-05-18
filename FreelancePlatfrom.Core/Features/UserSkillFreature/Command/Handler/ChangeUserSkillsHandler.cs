using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.UserSkillFreature.Command.Model;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.UserSkillFreature.Command.Handler
{
    public class ChangeUserSkillsHandler : ResponseHandler, IRequestHandler<ChangeUserSkillsCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserSkillesService _skillService;

        public ChangeUserSkillsHandler(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            IUserSkillesService skillService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _skillService = skillService;
        }

        public async Task<ApiResponse<string>> Handle(ChangeUserSkillsCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<string>("User ID not found in token.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<string>("User not found.");


            if (request.SelectedSkills != null && request.SelectedSkills.Count > 0)
            {
                var validSkillIds = await _skillService.GetValidSkillIdsAsync(request.SelectedSkills);
                if (validSkillIds == null || validSkillIds.Count != request.SelectedSkills.Count)
                    return BadRequest<string>("Invalid SkillId(s)");
            }

            try
            {

                if (request.SelectedSkills != null && request.SelectedSkills.Count > 0)
                {
                    var validSkillIds = await _skillService.GetValidSkillIdsAsync(request.SelectedSkills);
                    if (validSkillIds == null || validSkillIds.Count != request.SelectedSkills.Count)
                        return BadRequest<string>("Invalid SkillId(s)");

                    var existingSkillIds = await _skillService.GetUserSkillIdsAsync(userId);
                    var newSkillsToAdd = request.SelectedSkills.Except(existingSkillIds).ToList();

                    if (!newSkillsToAdd.Any())
                        return BadRequest<string>("All selected skills are already added.");

                    await _skillService.AddUserSkillsAsync(userId, request.SelectedSkills);
                }

                return Success("Skills updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Error updating skills: {ex.Message}");
            }
        }

     
    }
}