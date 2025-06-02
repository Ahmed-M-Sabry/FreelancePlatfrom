using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.JopPostFeatrures.Queries.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.JopPostFeatrures.Queries.Models
{
    public class SearchJobPostsQuery : IRequest<ApiResponse<List<SearchJobPostsDto>>>
    {
        public string Keyword { get; set; }
    }
}
