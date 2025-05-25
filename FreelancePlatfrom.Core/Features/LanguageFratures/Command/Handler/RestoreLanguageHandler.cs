using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.LanguageFratures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.LanguageFratures.Command.Handler
{
    public class RestoreLanguageHandler : ResponseHandler, IRequestHandler<RestoreLanguageCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILanguagesService _languageService;

        public RestoreLanguageHandler(UserManager<ApplicationUser> userManager,
                                      IHttpContextAccessor httpContextAccessor,
                                      ILanguagesService languageService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _languageService = languageService;
        }

        public async Task<ApiResponse<string>> Handle(RestoreLanguageCommand request, CancellationToken cancellationToken)
        {
            var language = await _languageService.GetLanguageByIdAsync(request.Id);
            if (language == null)
                return NotFound<string>($"Language with Id '{request.Id}' not found.");

            if (!language.IsDeleted)
                return BadRequest<string>($"Language with Id '{request.Id}' is not deleted.");

            language.IsDeleted = false;
            await _languageService.UpdateAsync(language);

            return Created<string>($"Language with Id '{request.Id}' has been restored.");
        }
    }
}
