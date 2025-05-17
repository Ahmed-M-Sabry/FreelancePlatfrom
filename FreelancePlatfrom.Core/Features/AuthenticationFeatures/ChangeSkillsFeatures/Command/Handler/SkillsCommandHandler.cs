using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.ChangeSkillsFeatures.Command.Model;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AuthenticationFeatures.ChangeSkillsFeatures.Command.Handler
{
    public class SkillsCommandHandler : ResponseHandler, IRequestHandler<ChangeSkillsCommand, ApiResponse<string>>
        ,IRequestHandler<DeleteSkillByIdCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISkillService _skillService;

        public SkillsCommandHandler(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            ISkillService skillService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _skillService = skillService;
        }

        public async Task<ApiResponse<string>> Handle(ChangeSkillsCommand request, CancellationToken cancellationToken)
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

                return Success<string>("Skills updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest<string>($"Error updating skills: {ex.Message}");
            }
        }

        public async Task<ApiResponse<string>> Handle(DeleteSkillByIdCommand request, CancellationToken cancellationToken)
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