using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.CountryFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.CountryFeatures.Queries.Result;
using FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Models;
using FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Results;
using FreelancePlatfrom.Core.Features.LanguagesFeatures.Query.GetAllLanguagesForAdmin;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Service.AbstractionServices;
using FreelancePlatfrom.Service.ImplementationServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.CountryFeatures.Queries.Handler
{
    public class GetAllCountryForUserHandler : ResponseHandler,
        IRequestHandler<GetAllCountryForUserQuery, ApiResponse<List<GetAllCountryForUserResponse>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICountryServices _countryServices;
        private readonly IMapper _mapper;
        public GetAllCountryForUserHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , ICountryServices countryServices
            , IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _countryServices = countryServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetAllCountryForUserResponse>>> Handle(GetAllCountryForUserQuery request, CancellationToken cancellationToken)
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

            var allCountry = await _countryServices.GetAllCountriesForUser(); // Include both deleted and not
            if (allCountry == null || allCountry.Count == 0)
                return NotFound<List<GetAllCountryForUserResponse>>("No Countries found.");

            var result = _mapper.Map<List<GetAllCountryForUserResponse>>(allCountry);

            return Success(result, new { TotalCount = result.Count() });
        }
    }
}
