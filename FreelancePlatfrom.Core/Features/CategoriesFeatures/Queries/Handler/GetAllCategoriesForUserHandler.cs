using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.CategoriesFeatures.Queries.Models;
using FreelancePlatfrom.Core.Features.CategoriesFeatures.Queries.Result;
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

namespace FreelancePlatfrom.Core.Features.CategoriesFeatures.Queries.Handler
{
    public class GetAllCategoriesForUserHandler : ResponseHandler
        , IRequestHandler<GetAllCategoriesForUserQuery, ApiResponse<List<GetAllCategoriesForUserResponse>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public GetAllCategoriesForUserHandler(UserManager<ApplicationUser> userManager
            , IHttpContextAccessor httpContextAccessor
            , IFavoritesFreelancerServices favoritesFreelancerServices
            , ICategoryService categoryService
            , IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<GetAllCategoriesForUserResponse>>> Handle(GetAllCategoriesForUserQuery request, CancellationToken cancellationToken)
        {
            var categories = await _categoryService.GetAllCategoriesForUserAsync();
            if (categories == null || !categories.Any())
                return NotFound<List<GetAllCategoriesForUserResponse>>("No Categories Found.");

            var result = _mapper.Map<List<GetAllCategoriesForUserResponse>>(categories);

            return Success(result, new { TotalCount = result.Count() });
            throw new NotImplementedException();
        }
    }
}
