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
    public class GetAllCategoriesForAdminHandler : ResponseHandler, IRequestHandler<GetAllCategoriesForAdminQuery, ApiResponse<List<GetAllCategoriesForAdminResponse>>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public GetAllCategoriesForAdminHandler(UserManager<ApplicationUser> userManager
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

        public async Task<ApiResponse<List<GetAllCategoriesForAdminResponse>>> Handle(GetAllCategoriesForAdminQuery request, CancellationToken cancellationToken)
        {            
            
            //var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("Uid")?.Value;
            //if (string.IsNullOrEmpty(userId))
            //    return BadRequest<string>("User ID not found in token.");

            //// Verify user exists
            //var user = await _userManager.FindByIdAsync(userId);
            //if (user == null)
            //    return BadRequest<string>("User not found.");

            //// Is Admin
            //if (!await _userManager.IsInRoleAsync(user, ApplicationRoles.Admin))
            //    return BadRequest<string>("You Must Be An Admin");

            // Get All Categories
            var categories = await _categoryService.GetAllCategoriesForAdminAsync();
            if(categories == null || !categories.Any())
                return NotFound<List<GetAllCategoriesForAdminResponse>>("No Categories Found.");

            var result = _mapper.Map<List<GetAllCategoriesForAdminResponse>>(categories);

            return Success(result, new { TotalCount = result.Count() });
        }
    }
}
