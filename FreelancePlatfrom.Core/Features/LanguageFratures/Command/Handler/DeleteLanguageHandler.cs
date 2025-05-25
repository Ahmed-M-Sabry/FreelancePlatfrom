using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.LanguageFratures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Shared;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.LanguageFratures.Command.Handler
{
    public class DeleteLanguageHandler : ResponseHandler, IRequestHandler<DeleteLanguageCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILanguagesService _languageService;

        public DeleteLanguageHandler(UserManager<ApplicationUser> userManager,
                                     IHttpContextAccessor httpContextAccessor,
                                     ILanguagesService languageService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _languageService = languageService;
        }

        public async Task<ApiResponse<string>> Handle(DeleteLanguageCommand request, CancellationToken cancellationToken)
        {
            //var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            //if (string.IsNullOrEmpty(userId))
            //    return BadRequest<string>("User ID not found in token.");

            //// Verify user exists
            //var user = await _userManager.FindByIdAsync(userId);
            //if (user == null)
            //    return BadRequest<string>("User not found.");

            //// Is Admin
            //if (!await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
            //    return BadRequest<string>("You Must Be An Admin");

            var language = await _languageService.GetLanguageByIdAsync(request.Id);
            if (language == null)
                return NotFound<string>($"Language with Id '{request.Id}' not found.");

            if (language.IsDeleted)
                return BadRequest<string>($"Language with Id '{request.Id}' is already deleted.");

            language.IsDeleted = true;
            await _languageService.UpdateAsync(language);

            return Deleted<string>($"Language with Id '{request.Id}' has been deleted.");
        }
    }
}
