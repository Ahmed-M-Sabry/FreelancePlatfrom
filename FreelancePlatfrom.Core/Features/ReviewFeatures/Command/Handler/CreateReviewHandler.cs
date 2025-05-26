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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Handler
{
    public class CreateReviewHandler : ResponseHandler,
    IRequestHandler<CreateReviewCommand, ApiResponse<string>>
    {
        private readonly IReviewService _reviewService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidator<CreateReviewCommand> _validator;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IContractService _contractService;

        public CreateReviewHandler(
            IReviewService reviewService,
            IHttpContextAccessor httpContextAccessor,
            IValidator<CreateReviewCommand> validator,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            IContractService contractService)
        {
            _reviewService = reviewService;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
            _mapper = mapper;
            _userManager = userManager;
            _contractService = contractService;
        }

        public async Task<ApiResponse<string>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var result = await _validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
                return ValidationFailed<string>(result.Errors.Select(x => x.ErrorMessage).ToList());

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized<string>();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound<string>("User not found");
            var Contract = await _contractService.GetContractWithAccpeted(request.ContractId, userId, request.FreelancerId);

            if (Contract is null)
                return NotFound<string>("No Contract To Review");

            if (Contract.Status != ContractStatus.Accepted)
                return BadRequest<string>("You Can't Rate Freelancer Before He Accepted Contract");

            var isAlreadyReviewed = await _reviewService.CheckIfReviewExists(request.ContractId, userId , request.FreelancerId);
            if (isAlreadyReviewed)
                return BadRequest<string>("You have already reviewed this contract.");

            if (Contract.EndDate > DateTime.UtcNow)
                return BadRequest<string>("You Can't Rate Freelancer Before He Deliver Task");
            // Mapping
            var review = _mapper.Map<Review>(request);
            review.ClientId = userId;

            await _reviewService.AddReviewAsync(review);

            return Success("Review created successfully");
        }
    }   
}
