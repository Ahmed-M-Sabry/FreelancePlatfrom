using AutoMapper;
using FreelancePlatfrom.Core.Features.CategoriesFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.SkillsFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.SkillsFeatures.Queries.Result;
using FreelancePlatfrom.Data.Entities.SkillAndCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Mapping
{
    public class SkillMappingProfile : Profile
    {

        public SkillMappingProfile() 
        {
            CreateMap<CreateSkillCommand , Skill >()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.SkillName))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false));

            CreateMap<EditSkillCommand, Skill>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Skill, GetAllSkillsForAdminResponse>();

            CreateMap<Skill, GetAllSkillsForUserResponse>();

        }

    }
}
