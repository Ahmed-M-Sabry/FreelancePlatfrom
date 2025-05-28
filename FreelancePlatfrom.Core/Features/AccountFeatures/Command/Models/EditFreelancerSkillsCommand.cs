using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AccountFeatures.Command.Models
{
    public class EditFreelancerSkillsCommand : IRequest<ApiResponse<string>>
    {
        public List<int> SkillIds { get; set; }
    }
}
