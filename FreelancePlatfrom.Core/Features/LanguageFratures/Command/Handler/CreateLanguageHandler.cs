using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.LanguageFratures.Command.Models;
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

namespace FreelancePlatfrom.Core.Features.LanguageFratures.Command.Handler
{
    public class CreateLanguageHandler : ResponseHandler, IRequestHandler<CreateLanguageCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILanguagesService _languagesService;
        private readonly IMapper _mapper;
        public CreateLanguageHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , ILanguagesService languagesService
            , IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _languagesService = languagesService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> Handle(CreateLanguageCommand request, CancellationToken cancellationToken)
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

            var isLanguageExist = await _languagesService.GetLanguageByIdAsync(request.Id);
            if (isLanguageExist != null && !isLanguageExist.IsDeleted)
                return BadRequest<string>($"Category {request.Value} With Id {isLanguageExist.Id} is Already Existed");

            if (isLanguageExist != null && isLanguageExist.IsDeleted)
                return BadRequest<string>($"Category {request.Value} With Id {isLanguageExist.Id} is Deleted Restore it");

            var language = _mapper.Map<Language>(request);

            await _languagesService.CreateAsync(language);

            return Created($"Language with Id {language.Id} created.");
        }
    }
}
