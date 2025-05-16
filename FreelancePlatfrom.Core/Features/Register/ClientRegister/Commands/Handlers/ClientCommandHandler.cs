using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.Register.ClientRegister.Commands.Model;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Shared;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.Register.ClientRegister.Commands.Handlers
{
    public class ClientCommandHandler : ResponseHandler
        , IRequestHandler<AddClientCommand, ApiResponse<string>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICountryServices _countryServices;
        private readonly IAuthenticatioService _authenticatioService;
        public ClientCommandHandler(IMapper mapper , UserManager<ApplicationUser> userManager 
                , ICountryServices countryServices , IAuthenticatioService authenticatioService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _countryServices = countryServices;
            _authenticatioService = authenticatioService;
        }

        public async Task<ApiResponse<string>> Handle(AddClientCommand request, CancellationToken cancellationToken)
        {
            // If Email is already registered, return an error response
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return BadRequest<string>("Email already registered");

            // If CountryId is not valid, return an error response
            var IsValidCountry = await _countryServices.GetCountryByIdAsync(request.CountryId);
            if (IsValidCountry == null)
                return BadRequest<string>("Invalid CountryId");

            // create a new user and assign the role of client
            var newUser = _mapper.Map<ApplicationUser>(request);

            if (newUser == null)
                return BadRequest<string>("Mapping failed.");

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ValidationFailed<string>(errors);
            }
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, ApplicationRoles.User);
            }
            var Token = await _authenticatioService.CreateJwtToken(newUser);
            return Created(Token , new { Role = ApplicationRoles.User});
        }
    }
}
