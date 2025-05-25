using AutoMapper;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Models;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using FreelancePlatfrom.Data.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Mapping
{
    public class ContractMappingProfile : Profile
    {

        public ContractMappingProfile() 
        {
            CreateMap<CreateContractCommand, Contracts>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ClientId, opt => opt.Ignore()) 
                .ForMember(dest => dest.ContractDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest=>dest.Status , opt => opt.MapFrom(_=>ContractStatus.Pending));


        }
    }
}
