using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.SkillsFeatures.Command.Models
{
    public class CreateSkillCommand : IRequest<ApiResponse<string>>
    {
        public string SkillName { get; set; }
        public CreateSkillCommand(string skillName)
        {
            SkillName = skillName;
        }
    }
}
