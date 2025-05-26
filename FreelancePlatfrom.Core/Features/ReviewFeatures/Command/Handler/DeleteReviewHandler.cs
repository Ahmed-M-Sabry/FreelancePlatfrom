using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Shared;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Handler
{
    public class DeleteReviewHandler : ResponseHandler,
        IRequestHandler<DeleteReviewCommand, ApiResponse<string>>
    {
        private readonly IReviewService _reviewService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public DeleteReviewHandler(
            IReviewService reviewService,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _reviewService = reviewService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<ApiResponse<string>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized<string>();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound<string>("User not found");

            var review = await _reviewService.GetByIdAsync(request.Id);
            if (review == null)
                return NotFound<string>("Review not found");

            if (review.ClientId != userId)
                return BadRequest<string>("You are not authorized to delete this review");

            await _reviewService.DeleteReviewAsync(review);

            return Deleted<string>("Review deleted successfully");
        }
    }
}
