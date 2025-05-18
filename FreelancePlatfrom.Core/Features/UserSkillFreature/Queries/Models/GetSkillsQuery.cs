using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using MediatR;
using System.Collections.Generic;

namespace FreelancePlatfrom.Core.Features.UserSkillFreature.Queries.Models
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
