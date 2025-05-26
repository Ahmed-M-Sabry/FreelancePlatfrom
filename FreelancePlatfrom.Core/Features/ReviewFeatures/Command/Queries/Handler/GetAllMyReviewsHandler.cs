using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Queries.Models;
using FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Queries.Results;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Queries.Handler
{
    public class GetAllMyReviewsHandler : ResponseHandler,
    IRequestHandler<GetAllMyReviewsQuery, ApiResponse<List<GetReviewResponse>>>
    {
        private readonly IReviewService _reviewService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetAllMyReviewsHandler(IReviewService reviewService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _reviewService = reviewService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetReviewResponse>>> Handle(GetAllMyReviewsQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized<List<GetReviewResponse>>();

            var reviews = await _reviewService.GetAllMyReviewsAsync(userId);
            if(reviews.Count() == 0 )
                return NotFound<List<GetReviewResponse>>();

            var result = _mapper.Map<List<GetReviewResponse>>(reviews);

            return Success(result ,new { TotalReview = reviews.Count()});
        }
    }

}
