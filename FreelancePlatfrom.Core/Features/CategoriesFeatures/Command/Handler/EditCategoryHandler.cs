using AutoMapper;
using FreelancePlatfrom.Core.Base;
using FreelancePlatfrom.Core.Features.CategoriesFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using FreelancePlatfrom.Service.AbstractionServices;
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
    public class EditCategoryHandler : ResponseHandler, IRequestHandler<EditCategoryCommand, ApiResponse<string>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public EditCategoryHandler(UserManager<ApplicationUser> userManager
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

        public async Task<ApiResponse<string>> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
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

            // Verfiy if Category is Existed
            var isCategoryExisted = await _categoryService.GetByIdAsync(request.Id);
            if (isCategoryExisted == null)
                return NotFound<string>($"Not Found With Is {request.Id}");

            if (isCategoryExisted.IsDeleted)
                return BadRequest<string>($"{request.Id} Is Deleted");

            _mapper.Map(request, isCategoryExisted);

            await _categoryService.UpdatedCategory(isCategoryExisted);

            return Created($"Edit Success {isCategoryExisted.Id}");
        }
    }
}
