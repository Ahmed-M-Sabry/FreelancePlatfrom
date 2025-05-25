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
    public class GetSkillByIdQuery : IRequest<ApiResponse<GetSkillByIdResponse>>
    {
        public int Id { get; set; }
        public GetSkillByIdQuery(int id )
        {
            Id = id;
        }

    }
}
