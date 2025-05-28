using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.FavoritesFreelancerFeatures.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.FavoritesFreelancerFeatures.Queries.Models
{
    public class GetMyFavoritesFreelancerQuery : IRequest<ApiResponse<List<GetMyFavoritesFreelancerResponse>>>
    {
    }
}
