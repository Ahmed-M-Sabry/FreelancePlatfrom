using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.AccountFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.AccountFeatures.Queries.Results;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Result;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Service.AbstractionServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.AccountFeatures.Queries.Handler
{
    public class GetFreelancerProfileHandler : ResponseHandler,
        IRequestHandler<GetFreelancerProfileQuery, ApiResponse<FreelancerProfileResponse>>
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IReviewService _reviewService;
        public GetFreelancerProfileHandler(IAccountService accountService,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            IMapper mapper
            ,IReviewService reviewService)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
            _reviewService = reviewService;
        }
        public async Task<ApiResponse<FreelancerProfileResponse>> Handle(GetFreelancerProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
            if (userId is null)
                return Unauthorized<FreelancerProfileResponse>();

            var UserDetails = await _accountService.GetApplicationFreelancerByIdAsync(userId);
            
            if (UserDetails is null)
                return NotFound<FreelancerProfileResponse>("User not found.");

            var freelancerProfile = _mapper.Map<FreelancerProfileResponse>(UserDetails);

            freelancerProfile.AverageRating = await _reviewService.GetFreelancerRateById(userId);

            return Success<FreelancerProfileResponse>(freelancerProfile, new { UserId = UserDetails.Id });
        }
    }
}
