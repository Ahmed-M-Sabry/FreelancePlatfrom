using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Models
{
    public class RejectContractCommand : IRequest<ApiResponse<string>>
    {
        public int ContractId { get; set; }
        public RejectContractCommand(int id)
        {
            ContractId = id;
        }
    }
}
