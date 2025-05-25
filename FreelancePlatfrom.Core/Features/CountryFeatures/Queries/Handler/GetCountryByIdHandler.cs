using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.CountryFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.CountryFeatures.Queries.Result;
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

namespace FreelancePlatfrom.Core.Features.CountryFeatures.Queries.Handler
{
    public class GetCountryByIdHandler : ResponseHandler
        , IRequestHandler<GetCountryByIdQuery, ApiResponse<GetCountryByIdResponse>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICountryServices _countryServices;
        private readonly IMapper _mapper;
        public GetCountryByIdHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , ICountryServices countryServices
            , IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _countryServices = countryServices;
            _mapper = mapper;
        }
        public async Task<ApiResponse<GetCountryByIdResponse>> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
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



            var country = await _countryServices.GetCountryByIdAsync(request.Id);
            if (country is null)
                return NotFound<GetCountryByIdResponse>($"No Country Found With Id : {request.Id}");

            var existCountry = _mapper.Map<GetCountryByIdResponse>(country);

            return Success<GetCountryByIdResponse>(existCountry);
        }
    }
}
