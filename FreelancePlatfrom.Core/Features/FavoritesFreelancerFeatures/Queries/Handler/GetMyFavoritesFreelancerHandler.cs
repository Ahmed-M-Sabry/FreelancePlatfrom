using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.FavoritesFreelancerFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.FavoritesFreelancerFeatures.Queries.Results;
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

namespace FreelancePlatfrom.Core.Features.FavoritesFreelancerFeatures.Queries.Handler
{
    public class GetMyFavoritesFreelancerHandler :
        ResponseHandler, IRequestHandler<GetMyFavoritesFreelancerQuery, ApiResponse<List<GetMyFavoritesFreelancerResponse>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFavoritesFreelancerServices _favoritesFreelancerServices;
        private readonly IMapper _mapper;
        public GetMyFavoritesFreelancerHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , IFavoritesFreelancerServices favoritesFreelancerServices
            ,IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _favoritesFreelancerServices = favoritesFreelancerServices;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetMyFavoritesFreelancerResponse>>> Handle(GetMyFavoritesFreelancerQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            if (string.IsNullOrEmpty(userId))
                return BadRequest<List<GetMyFavoritesFreelancerResponse>>("User ID not found in token.");

            var FavoritesFreelancers = await _favoritesFreelancerServices.GetAllFreelancersFavoritedByClient(userId);
            if (FavoritesFreelancers == null || !FavoritesFreelancers.Any())
                return NotFound<List<GetMyFavoritesFreelancerResponse>>("No freelancers found in favorites.");

            var response = _mapper.Map<List<GetMyFavoritesFreelancerResponse>>(FavoritesFreelancers);

            return Success<List<GetMyFavoritesFreelancerResponse>>(response, new { TotalCount = response.Count() });
        }
    }
}
