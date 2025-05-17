using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.GetSkillsFeatures.Query.Model;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AuthenticationFeatures.GetSkillsFeatures.Query.Handler
{
    public class GetSkillsQueryHandler : ResponseHandler, IRequestHandler<GetSkillsQuery, ApiResponse<List<SkillLiteDto>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISkillService _skillService;

        public GetSkillsQueryHandler(
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            ISkillService skillService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _skillService = skillService;
        }

        public async Task<ApiResponse<List<SkillLiteDto>>> Handle(GetSkillsQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<List<SkillLiteDto>>("User ID not found in token.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest<List<SkillLiteDto>>("User not found.");

            var userSkills = await _skillService.GetUserSkillsWithNamesAndIdAsync(userId);

            if (userSkills == null || !userSkills.Any())
                return BadRequest<List<SkillLiteDto>>("No skills found for You.");


            var result = userSkills.Select(s => new SkillLiteDto
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();

            return Success(result);
        }
    }
}
