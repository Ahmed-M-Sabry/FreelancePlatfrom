using AutoMapper;
using FluentValidation;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.Rating;
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
    public class EditReviewHandler : ResponseHandler,
        IRequestHandler<EditReviewCommand, ApiResponse<string>>
    {
        private readonly IReviewService _reviewService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidator<EditReviewCommand> _validator;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public EditReviewHandler(
            IReviewService reviewService,
            IHttpContextAccessor httpContextAccessor,
            IValidator<EditReviewCommand> validator,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _reviewService = reviewService;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ApiResponse<string>> Handle(EditReviewCommand request, CancellationToken cancellationToken)
        {
            var validation = await _validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
                return ValidationFailed<string>(validation.Errors.Select(x => x.ErrorMessage).ToList());

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
                return BadRequest<string>("You are not authorized to edit this review");

            _mapper.Map(request, review);


            await _reviewService.UpdateReview(review);

            return Success("Review updated successfully");
        }
    }
}
