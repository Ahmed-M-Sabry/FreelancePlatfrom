using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Models
{
    public class AcceptContractCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public AcceptContractCommand(int id)
        {
            Id = id;
        }
    }
}
