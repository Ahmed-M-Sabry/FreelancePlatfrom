using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.AccountFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FreelancePlatfrom.Core.Features.AccountFeatures.Command.Handler
{
    public class EditFreelancerLanguagesCommandHandler
        : ResponseHandler, IRequestHandler<EditFreelancerLanguagesCommand, ApiResponse<string>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILanguagesService _languagesService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditFreelancerLanguagesCommandHandler(
            IHttpContextAccessor httpContextAccessor,
            ILanguagesService languagesService,
            UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _languagesService = languagesService;
            _userManager = userManager;
        }

        public async Task<ApiResponse<string>> Handle(EditFreelancerLanguagesCommand request, CancellationToken cancellationToken)
        {
            // Get user ID from token
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<string>("User ID not found in token.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<string>("User not found.");

            // Ensure at least one language is provided
            if (request.LanguageIds == null || !request.LanguageIds.Any())
                return BadRequest<string>("At least one language must be selected.");

            // Validate provided language IDs
            var validLanguageIds = await _languagesService.GetValidLanguageNamesAsync(request.LanguageIds);
            if (validLanguageIds == null || validLanguageIds.Count != request.LanguageIds.Count)
                return BadRequest<string>("One or more language IDs are invalid Or Already Exist.");

            // Get current user language IDs
            var currentLanguageIds = await _languagesService.GetUserLanguageIdsAsync(userId);

            // Determine which languages to add or remove
            var toAdd = request.LanguageIds.Except(currentLanguageIds).ToList();
            var toRemove = currentLanguageIds.Except(request.LanguageIds).ToList();

            // Remove old languages
            if (toRemove.Any())
                await _languagesService.RemoveUserLanguagesAsync(userId, toRemove);

            // Add new languages
            if (toAdd.Any())
            {
                var newUserLanguages = toAdd.Select(languageId => new ApplicationUserLanguage
                {
                    ApplicationUserId = userId,
                    LanguageId = languageId
                }).ToList();

                await _languagesService.AddUserLanguagesAsync(newUserLanguages);
            }

            return Success<string>("Freelancer languages updated successfully.");
        }
    }
}
