using AutoMapper;
using FreelancePlatfrom.Core.Features.LanguageFratures.Command.Models;
using FreelancePlatfrom.Core.Features.LanguageFratures.Queries.Results;
using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Mapping
{
    public class LanguageMappingProfile : Profile
    {
        public LanguageMappingProfile()
        {
            // Create
            CreateMap<CreateLanguageCommand, Language>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));

            // Edit
            CreateMap<EditLanguageCommand, Language>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            CreateMap<Language, GetAllLanguagesForAdminResponse>();

            CreateMap<Language, GetAllLanguagesForUserResponse>();

            CreateMap<Language, GetLanguageByIdResponse>();

            CreateMap<Language, GetLanguageByNameResponse>();



        }
    }
}
