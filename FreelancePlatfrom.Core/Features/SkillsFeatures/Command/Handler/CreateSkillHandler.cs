using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.SkillsFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using FreelancePlatfrom.Service.AbstractionServices;
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
    public class CreateSkillHandler : ResponseHandler, IRequestHandler<CreateSkillCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISkillService _skillService;
        private readonly IMapper _mapper;
        public CreateSkillHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , ISkillService skillService
            , IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _skillService = skillService;
            _mapper = mapper;
        }
        public async Task<ApiResponse<string>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
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
            var isSkillExisted = await _skillService.GetByNameAsync(request.SkillName);

            if (isSkillExisted != null && !isSkillExisted.IsDeleted)
                return BadRequest<string>($"Skill : {request.SkillName} With Id {isSkillExisted.Id} is Already Existed");

            if (isSkillExisted != null && isSkillExisted.IsDeleted)
                return BadRequest<string>($"Skill :  {request.SkillName} With Id {isSkillExisted.Id} is Deleted");

            var NewSkill = _mapper.Map<Skill>(request);
            if (NewSkill is null)
                return BadRequest<string>("Error Happen while Mapping");

            await _skillService.CreateNewSkill(NewSkill);

            return Created<string>($"Skill With Name = {request.SkillName} is Created Successfully");
            throw new NotImplementedException();
        }
    }
}
