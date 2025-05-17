using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AuthenticationFeatures.ChangeSkillsFeatures.Command.Model
{
    public class DeleteSkillByIdCommand : IRequest<ApiResponse<string>>
    {
        public int SkillId { get; set; }
    }
}
