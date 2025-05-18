using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.Register.FreelancerRegister.Model;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Shared;
using FreelancePlatfrom.Service.AbstractionServices;
using FreelancePlatfrom.Service.ImplementationServices;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AuthenticationFeatures.Register.FreelancerRegister.Handler
{
    public class FreelancerCommandHandler : ResponseHandler , IRequestHandler<AddFreelancerCommand ,ApiResponse<string>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICountryServices _countryServices;
        private readonly IAuthenticatioService _authenticatioService;
        private readonly IUserSkillesService _userSkillService;
        private readonly IUserLanguagesService _languageService;
        private readonly IFileService _fileService;
        private readonly ISkillService _skillService;

        public FreelancerCommandHandler(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            ICountryServices countryServices,
            IAuthenticatioService authenticatioService,
            IUserSkillesService UserskillService,
            IUserLanguagesService languageService,
            ISkillService skillService,
            IFileService fileService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _countryServices = countryServices;
            _authenticatioService = authenticatioService;
            _userSkillService = UserskillService;
            _languageService = languageService;
            _fileService = fileService;
            _skillService = skillService;
        }

        public async Task<ApiResponse<string>> Handle(AddFreelancerCommand request, CancellationToken cancellationToken)
        {
            // If Email is already registered, return an error response
            var existingUser =await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return BadRequest<string>("Email already registered");

            // If CountryId is not valid, return an error response
            var IsValidCountry = await _countryServices.GetCountryByIdAsync(request.CountryId);
            if (IsValidCountry == null)
                return BadRequest<string>("Invalid CountryId");

            // If SelectedLanguages is not valid, return an error response
            if (request.SelectedLanguages != null && request.SelectedLanguages.Count > 0)
            {
                var IsValidLanguage = await _languageService.GetValidLanguageNamesAsync(request.SelectedLanguages);
                if (IsValidLanguage == null || IsValidLanguage.Count != request.SelectedLanguages.Count)
                    return BadRequest<string>("Invalid LanguageId(s)");
            }
            // If SelectedSkills is not valid, return an error response
            if(request.SelectedSkills != null && request.SelectedSkills.Count > 0)
            {
                var IsValidSkill = await _skillService.GetValidSkillIdsAsync(request.SelectedSkills);
                if (IsValidSkill == null || IsValidSkill.Count != request.SelectedSkills.Count)
                    return BadRequest<string>("Invalid SkillId(s)");
            }

            if (request.ProfilePicture != null)
            {
                var filePath = await _fileService.UploadFileAsync("Uploads/ProfilePictures", request.ProfilePicture);
                request.ProfilePicturePath = filePath;
            }

            // create a new user and assign the role of Freelancer
            var newUser = _mapper.Map<ApplicationUser>(request);
            if(newUser is null)
                return BadRequest<string>("Mapping failed.");


            // Create the user with the provided password
            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ValidationFailed<string>(errors);
            }
            var roleResult = await _userManager.AddToRoleAsync(newUser, "Freelancer");
            if (!roleResult.Succeeded)
            {
                // Optionally delete the user if role assignment fails
                await _userManager.DeleteAsync(newUser);
                return BadRequest<string>("Failed to assign Freelancer role.");
            }


            var Token = await _authenticatioService.CreateJwtToken(newUser);
            return Created(Token, new { Role = ApplicationRoles.User });
        }
    }
}
