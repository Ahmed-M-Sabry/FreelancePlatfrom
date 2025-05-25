using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.SkillsFeatures.Command.Models;
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

namespace FreelancePlatfrom.Core.Features.SkillsFeatures.Command.Handler
{
    public class DeleteSkillHandler : ResponseHandler, IRequestHandler<DeleteSkillCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISkillService _skillService;
        private readonly IMapper _mapper;
        public DeleteSkillHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , IFavoritesFreelancerServices favoritesFreelancerServices
            , ISkillService skillService
            , IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _skillService = skillService;
            _mapper = mapper;
        }
        public async Task<ApiResponse<string>> Handle(DeleteSkillCommand request, CancellationToken cancellationToken)
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

            // Verfiy if Skill is Existed
            var isSkillExisted = await _skillService.GetByIdAsync(request.Id);

            if (isSkillExisted == null)
                return NotFound<string>($"Not Found With Is {request.Id}");

            if (isSkillExisted.IsDeleted)
                return BadRequest<string>($"{request.Id} Is Deleted Yoy Can R");

            await _skillService.DeleteSkill(request.Id);

            return Deleted<string>($"Skill With id = {request.Id} is Deleted");
        }
    }
}
