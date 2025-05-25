using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.LanguageFratures.Command.Models;
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

namespace FreelancePlatfrom.Core.Features.LanguageFratures.Command.Handler
{
    public class EditLanguageHandler : ResponseHandler, IRequestHandler<EditLanguageCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILanguagesService _languageService;
        private readonly IMapper _mapper;
        public EditLanguageHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , ILanguagesService languagesService
            , IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _languageService = languagesService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(EditLanguageCommand request, CancellationToken cancellationToken)
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
                return BadRequest<string>($"Language '{request.Id}' is deleted. You must restore it first.");

            _mapper.Map(request, language);

            await _languageService.UpdateAsync(language);

            return Created($"Language with Id {language.Id} updated.");
        }
    }

}
