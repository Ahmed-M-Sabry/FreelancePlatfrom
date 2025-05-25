using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.CategoriesFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.CategoriesFeatures.Queries.Result;
using FreelancePlatfrom.Core.Features.SkillsFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.SkillsFeatures.Queries.Result;
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

namespace FreelancePlatfrom.Core.Features.SkillsFeatures.Queries.Handler
{
    public class GetAllSkillsForAdminHandler : ResponseHandler
        , IRequestHandler<GetAllSkillsForAdminQuery, ApiResponse<List<GetAllSkillsForAdminResponse>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISkillService _skillService;
        private readonly IMapper _mapper;
        public GetAllSkillsForAdminHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , ISkillService skillService
            , IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _skillService = skillService;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<GetAllSkillsForAdminResponse>>> Handle(GetAllSkillsForAdminQuery request, CancellationToken cancellationToken)
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


            var Skills = await _skillService.GetAllSKillForAdminAsync();
            if (Skills == null || !Skills.Any())
                return NotFound<List<GetAllSkillsForAdminResponse>>("No SKills Found.");

            var result = _mapper.Map<List<GetAllSkillsForAdminResponse>>(Skills);

            return Success(result, new { TotalCount = result.Count() });
            throw new NotImplementedException();
        }
    }
}
