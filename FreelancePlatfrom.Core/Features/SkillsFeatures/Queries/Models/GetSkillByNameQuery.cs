using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.SkillsFeatures.Queries.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.SkillsFeatures.Queries.Models
{
    public class GetSkillByNameQuery : IRequest<ApiResponse<GetSkillByNameResponse>>
    {
        public string Name { get; set; }

        public GetSkillByNameQuery(string name)
        {
            Name = name;
        }
    }
}
