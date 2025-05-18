using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.UserSkillFreature.Command.Model
{
    public class ChangeUserSkillsCommand : IRequest<ApiResponse<string>>
    {
        public List<int> SelectedSkills { get; set; } = new List<int>();
    }

}
