using AutoMapper;
using FreelancePlatfrom.Core.Features.ReportsFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.ReportsFeatures.Queries.Results;
using FreelancePlatfrom.Data.Entities.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Mapping
{
    public class ReportMappingProfile : Profile
    {
        public ReportMappingProfile()
        {
            CreateMap<CreateReportCommand, Reports>();

            CreateMap<Reports, GetReportResponse>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.FirstName + " "+ src.Client.LastName))
                .ForMember(dest => dest.FreelancerName, opt => opt.MapFrom(src => src.Freelancer.FirstName + " " + src.Freelancer.LastName));
        }
    }
}
