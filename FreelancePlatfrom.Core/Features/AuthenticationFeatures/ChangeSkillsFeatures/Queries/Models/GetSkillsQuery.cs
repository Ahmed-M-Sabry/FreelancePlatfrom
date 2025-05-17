using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using MediatR;
using System.Collections.Generic;

namespace FreelancePlatfrom.Core.Features.AuthenticationFeatures.GetSkillsFeatures.Query.Model
{
    public class GetSkillsQuery : IRequest<ApiResponse<List<SkillLiteDto>>>
    {

    }
    public class SkillLiteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
