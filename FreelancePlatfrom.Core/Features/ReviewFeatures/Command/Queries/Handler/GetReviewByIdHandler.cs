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
    public class GetReviewByIdHandler : ResponseHandler,
    IRequestHandler<GetReviewByIdQuery, ApiResponse<GetReviewByIdResponse>>
    {
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetReviewByIdHandler(IReviewService reviewService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _reviewService = reviewService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<GetReviewByIdResponse>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized<GetReviewByIdResponse>();

            var review = await _reviewService.GetReviewWithUsersAsync(request.ReviewId);

            if (review == null)
                return NotFound<GetReviewByIdResponse>("Review not found");

            if (review.ClientId != userId && review.FreelancerId != userId)
                return Unauthorized<GetReviewByIdResponse>();

            var response = _mapper.Map<GetReviewByIdResponse>(review);

            return Success(response);
        }
    }

}
