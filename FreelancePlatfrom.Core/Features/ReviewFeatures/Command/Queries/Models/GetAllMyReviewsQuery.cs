using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Queries.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Queries.Models
{
    public class GetAllMyReviewsQuery : IRequest<ApiResponse<List<GetReviewResponse>>> { }

}
