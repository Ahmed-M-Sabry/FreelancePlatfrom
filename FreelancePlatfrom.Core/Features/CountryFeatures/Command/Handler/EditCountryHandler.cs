using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.CountryFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.RegisterNeeded;
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

namespace FreelancePlatfrom.Core.Features.CountryFeatures.Command.Handler
{
    public class EditCountryHandler : ResponseHandler
        , IRequestHandler<EditCountryCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICountryServices _countryServices;
        private readonly IMapper _mapper;
        public EditCountryHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , ICountryServices countryServices
            , IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _countryServices = countryServices;
            _mapper = mapper;
        }
        public async Task<ApiResponse<string>> Handle(EditCountryCommand request, CancellationToken cancellationToken)
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
            if (country == null)
                return NotFound<string>($"Country with Id '{request.Id}' not found.");

            if (country.IsDeleted)
                return BadRequest<string>($"Country '{request.Id}' is deleted. You must restore it first.");

            _mapper.Map(request, country);

            await _countryServices.UpdateCountry(country);

            return Created($"Country with Id {country.Id} updated.");
        }
    }
}
