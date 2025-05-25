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
    public class GetAllSkillsForUserHandler : ResponseHandler
        , IRequestHandler<GetAllSkillsForUserQuery, ApiResponse<List<GetAllSkillsForUserResponse>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISkillService _skillService;
        private readonly IMapper _mapper;
        public GetAllSkillsForUserHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , ISkillService skillService
            , IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _skillService = skillService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetAllSkillsForUserResponse>>> Handle(GetAllSkillsForUserQuery request, CancellationToken cancellationToken)
        {

            var Skills = await _skillService.GetAllSKillForUserAsync();
            if (Skills == null || !Skills.Any())
                return NotFound<List<GetAllSkillsForUserResponse>>("No Categories Found.");

            var result = _mapper.Map<List<GetAllSkillsForUserResponse>>(Skills);

            return Success(result, new { TotalCount = result.Count() });
        }
    }
}

