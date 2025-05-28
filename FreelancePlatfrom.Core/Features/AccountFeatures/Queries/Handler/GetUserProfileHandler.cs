using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.AccountFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.AccountFeatures.Queries.Results;
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
    public class GetUserProfileHandler : ResponseHandler
        , IRequestHandler<GetUserProfileQuery, ApiResponse<GetUserProfileResponse>>
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public GetUserProfileHandler(IAccountService accountService,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<ApiResponse<GetUserProfileResponse>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
            if (userId is null)
                return Unauthorized<GetUserProfileResponse>();

            var UserDetails = await _accountService.GetApplicationFreelancerByIdAsync(userId);

            if (UserDetails is null)
                return NotFound<GetUserProfileResponse>("User not found.");

            var userProfile = _mapper.Map<GetUserProfileResponse>(UserDetails);

            return Success<GetUserProfileResponse>(userProfile, new { UserId = UserDetails.Id });

        }
    }
}
