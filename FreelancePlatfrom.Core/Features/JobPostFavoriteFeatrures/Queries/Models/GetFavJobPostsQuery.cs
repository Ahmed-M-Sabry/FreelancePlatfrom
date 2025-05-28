using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.JobPostFavoriteFeatrures.Queries.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.JobPostFavoriteFeatrures.Queries.Models
{
    public class GetFavJobPostsQuery : IRequest<ApiResponse<List<FavJobPostResponse>>>
    {

    }
}
