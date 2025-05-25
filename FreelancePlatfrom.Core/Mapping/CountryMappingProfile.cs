using AutoMapper;
using FreelancePlatfrom.Core.Features.CountryFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.CountryFeatures.Queries.Result;
using FreelancePlatfrom.Data.Entities;
using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Mapping
{
    public class CountryMappingProfile : Profile
    {
        public CountryMappingProfile()
        {
            CreateMap<CreateCountryCommand, Country>()
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));

            CreateMap<EditCountryCommand, Country>()
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));

            CreateMap<Country, GetCountryByIdResponse>();
            CreateMap<Country, GetCountryByNameResponse>();
            CreateMap<Country, GetAllCountryForAdminResponse>();
            CreateMap<Country, GetAllCountryForUserResponse>();

        }
    }
}
