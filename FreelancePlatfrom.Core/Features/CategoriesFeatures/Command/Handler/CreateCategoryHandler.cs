using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.CategoriesFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using FreelancePlatfrom.Data.Shared;
using FreelancePlatfrom.Service.AbstractionServices;
using FreelancePlatfrom.Service.ImplementationServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Features.CategoriesFeatures.Command.Handler
{
    public class CreateCategoryHandler : ResponseHandler, IRequestHandler<CreateCategoryCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CreateCategoryHandler(UserManager<ApplicationUser> userManager
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

        public async Task<ApiResponse<string>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
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

            // Verify Category Name
            var isCategoryExist = await _categoryService.GetCategoryByNameAsync(request.CategoryName);
            if (isCategoryExist != null && !isCategoryExist.IsDeleted)
                return BadRequest<string>($"Category {request.CategoryName} With Id {isCategoryExist.Id} is Already Existed");

            if(isCategoryExist != null && isCategoryExist.IsDeleted)
                return BadRequest<string>($"Category {request.CategoryName} With Id {isCategoryExist.Id} is Deleted Restore it");

            var NewCategory = _mapper.Map<Category>(request);
            if (NewCategory is null)
                return BadRequest<string>("Error Happen while Mapping");

            await _categoryService.CreateNewCategory(NewCategory);

            return Created<string>($"New Category is Created {request.CategoryName}");

        }
    }
}
