﻿using AutoMapper;
using FreelancePlatfrom.Core.Features.PortfolioFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.PortfolioFeatures.Queries.Response;
using FreelancePlatfrom.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Mapping
{
    public class PortfolioMappingProfile : Profile
    {
        public PortfolioMappingProfile()
        {
            CreateMap<CreatePortfolioCommand, Portfolio>();

            CreateMap<EditPortfolioCommand, Portfolio>()
            .ForMember(dest => dest.Media, opt => opt.Ignore()) // Media handled manually
            .ForMember(dest => dest.UserId, opt => opt.Ignore()) // Not updated by command
            .ForMember(dest => dest.ApplicationUser, opt => opt.Ignore()); // Navigation property

            CreateMap<Portfolio, FreelancerPortfolioResponse>()
                .ForMember(dest => dest.FreelancerName,
                        opt => opt.MapFrom(src=>src.ApplicationUser.FirstName + " " + src.ApplicationUser.LastName));

            CreateMap<Portfolio, PortfolioByIdResponse>()
                .ForMember(dest => dest.FreelancerName,
                        opt => opt.MapFrom(src => src.ApplicationUser.FirstName + " " + src.ApplicationUser.LastName));

            CreateMap<Portfolio, MyPortfoliosResponse>();

        }
    }
}
