using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.AccountFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.AccountFeatures.Queries.Results;
using FreelancePlatfrom.Core.Mappings;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Shared;
using FreelancePlatfrom.Service.AbstractionServices;
using FreelancePlatfrom.Service.ImplementationServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FreelancePlatfrom.Core.Features.AccountFeatures.Queries.Handler
{
    public class SearchFreelancersHandler : ResponseHandler,
        IRequestHandler<SearchFreelancersQuery, ApiResponse<List<SearchFreelancerResponse>>>
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IReviewService _reviewService;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public SearchFreelancersHandler(IAccountService accountService, IMapper mapper 
            , UserManager<ApplicationUser> userManager
            , IReviewService reviewService , IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _mapper = mapper;
            _userManager = userManager;
            _reviewService = reviewService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResponse<List<SearchFreelancerResponse>>> Handle(SearchFreelancersQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst("uid")?.Value;
            if (userId is null)
                return Unauthorized<List<SearchFreelancerResponse>>();

            if (string.IsNullOrWhiteSpace(request.Keyword))
                return BadRequest<List<SearchFreelancerResponse>>("Keyword is required.");

            var freelancers = await _accountService.SearchFreelancersAsync(request.Keyword);

            if (freelancers == null || !freelancers.Any())
                return NotFound<List<SearchFreelancerResponse>>("No freelancers found.");

            var resultList = new List<SearchFreelancerResponse>();

            foreach (var user in freelancers)
            {
                if (await _userManager.IsInRoleAsync(user, ApplicationRoles.Freelancer))
                {
                    var response = _mapper.Map<SearchFreelancerResponse>(user);
                    response.AverageRating = await _reviewService.GetFreelancerRateById(user.Id);
                    resultList.Add(response);
                }
            }

            if (!resultList.Any())
                return NotFound<List<SearchFreelancerResponse>>("No freelancers with proper role found.");

            return Success(resultList, new { TotalCount = resultList.Count });
        }
    }
}
