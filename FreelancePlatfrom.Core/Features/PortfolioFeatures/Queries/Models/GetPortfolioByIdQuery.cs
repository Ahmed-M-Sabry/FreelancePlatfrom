using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.PortfolioFeatures.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.PortfolioFeatures.Queries.Models
{
    public class GetPortfolioByIdQuery : IRequest<ApiResponse<PortfolioByIdResponse>>
    {
        public int Id { get; set; }
        public GetPortfolioByIdQuery(int id) => Id = id;
    }
}
