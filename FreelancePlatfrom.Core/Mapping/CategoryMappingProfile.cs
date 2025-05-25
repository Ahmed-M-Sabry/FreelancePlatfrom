using AutoMapper;
using FreelancePlatfrom.Core.Features.CategoriesFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.CategoriesFeatures.Queries.Result;
using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Mapping
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile() 
        {
            CreateMap<CreateCategoryCommand, Category>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

            CreateMap<EditCategoryCommand, Category>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


            CreateMap<Category, GetAllCategoriesForAdminResponse>();

            CreateMap<Category, GetAllCategoriesForUserResponse>();

            CreateMap<Category, GetCategoryByIdResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

            CreateMap<Category, GetCategoryByNameResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted));

        }
    }
}
