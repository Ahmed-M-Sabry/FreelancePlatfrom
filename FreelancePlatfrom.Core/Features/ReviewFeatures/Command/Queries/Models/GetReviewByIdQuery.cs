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
    public class GetReviewByIdQuery : IRequest<ApiResponse<GetReviewByIdResponse>>
    {
        public int ReviewId { get; set; }

        public GetReviewByIdQuery(int reviewId)
        {
            ReviewId = reviewId;
        }
    }

}
