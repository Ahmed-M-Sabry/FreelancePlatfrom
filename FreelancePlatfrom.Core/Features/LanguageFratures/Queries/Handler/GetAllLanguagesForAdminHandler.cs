using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Results;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.LanguagesFeatures.Query.GetAllLanguagesForAdmin
{
    public class GetAllLanguagesForAdminHandler : ResponseHandler,
        IRequestHandler<GetAllLanguagesForAdminQuery, ApiResponse<List<GetAllLanguagesForAdminResponse>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILanguagesService _languagesService;
        private readonly IMapper _mapper;
        public GetAllLanguagesForAdminHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , ILanguagesService languagesService
            , IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _languagesService = languagesService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetAllLanguagesForAdminResponse>>> Handle(GetAllLanguagesForAdminQuery request, CancellationToken cancellationToken)
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

            var allLanguages = await _languagesService.GetAllLanguagesAsync(); // Include both deleted and not
            if(allLanguages == null || allLanguages.Count == 0)
                return NotFound<List<GetAllLanguagesForAdminResponse>>("No languages found.");

            var result = _mapper.Map<List<GetAllLanguagesForAdminResponse>>(allLanguages);
            
            return Success(result, new { TotalCount = result.Count() });
        }
    }
}
