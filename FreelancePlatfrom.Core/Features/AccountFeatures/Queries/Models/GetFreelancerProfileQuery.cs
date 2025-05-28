using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.AccountFeatures.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AccountFeatures.Queries.Models
{
    public class GetFreelancerProfileQuery : IRequest<ApiResponse<FreelancerProfileResponse>>
    {
    }
}
