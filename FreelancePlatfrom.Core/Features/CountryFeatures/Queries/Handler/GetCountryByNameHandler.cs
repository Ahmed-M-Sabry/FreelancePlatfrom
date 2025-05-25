using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.CountryFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.CountryFeatures.Queries.Result;
using FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Results;
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
    public class GetCountryByNameHandler : ResponseHandler
        , IRequestHandler<GetCountryByNameQuery, ApiResponse<GetCountryByNameResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICountryServices _countryServices;
        private readonly IMapper _mapper;
        public GetCountryByNameHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , ICountryServices countryServices
            , IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _countryServices = countryServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<GetCountryByNameResponse>> Handle(GetCountryByNameQuery request, CancellationToken cancellationToken)
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



            var country = await _countryServices.GetCountryByNameAsync(request.Name);
            if (country is null)
                return NotFound<GetCountryByNameResponse>($"No Country Found With name : {request.Name}");

            var existCountry = _mapper.Map<GetCountryByNameResponse>(country);

            return Success<GetCountryByNameResponse>(existCountry);
        }
    }
}
