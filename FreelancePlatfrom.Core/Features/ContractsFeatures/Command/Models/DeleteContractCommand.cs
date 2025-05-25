using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Models
{
    public class DeleteContractCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }
        public DeleteContractCommand(int id)
        {
            Id = id;
        }
    }
}
