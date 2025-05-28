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
    public class GetFreelancerPortfolioQuery : IRequest<ApiResponse<List<FreelancerPortfolioResponse>>>
    {
        public string FreelancerId { get; set; }

        public GetFreelancerPortfolioQuery(string freelancerId)
        {
            FreelancerId = freelancerId;
        }
    }
}
