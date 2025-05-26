using FreelancePlatfrom.Core.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.PortfolioFeatures.Command.Models
{
    public class DeletePortfolioCommand : IRequest<ApiResponse<string>>
    {
        public int Id { get; set; }

        public DeletePortfolioCommand(int id)
        {
            Id = id;
        }
    }
}
