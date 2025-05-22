using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Models
{
    public class AcceptApplyTaskCommand: IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public AcceptApplyTaskCommand(int id)
        {
            Id = id;
        }

    }
}
