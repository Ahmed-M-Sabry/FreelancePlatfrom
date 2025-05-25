using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Models;
using FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Results;
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

namespace FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Handler
{
    public class GetAllLanguagesForUserHandler : ResponseHandler, 
      IRequestHandler<GetAllLanguagesForUserQuery, ApiResponse<List<GetAllLanguagesForUserResponse>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILanguagesService _languagesService;
        private readonly IMapper _mapper;
        public GetAllLanguagesForUserHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , ILanguagesService languagesService
            , IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _languagesService = languagesService;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<GetAllLanguagesForUserResponse>>> Handle(GetAllLanguagesForUserQuery request, CancellationToken cancellationToken)
        {
            var allLanguages = await _languagesService.GetAllUserLanguagesAsync();
            if (allLanguages == null || allLanguages.Count == 0)
                return NotFound<List<GetAllLanguagesForUserResponse>>("No languages found.");

            var result = _mapper.Map<List<GetAllLanguagesForUserResponse>>(allLanguages);

            return Success(result , new { TotalCount = result.Count()});
        }
    }
}
