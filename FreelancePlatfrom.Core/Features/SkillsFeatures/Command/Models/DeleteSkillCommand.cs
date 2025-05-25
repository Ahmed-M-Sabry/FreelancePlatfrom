using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.SkillsFeatures.Command.Models
{
    public class DeleteSkillCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public DeleteSkillCommand(int id)
        {
            Id = id;
        }
    }
}
