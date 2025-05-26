using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ContractsFeatures.Queries.Models
{
    public class GetMyContractsQuery : IRequest<ApiResponse<List<GetMyContractsResponse>>>
    {

    }
}
