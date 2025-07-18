﻿using AutoMapper;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Queries.Results;
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

            CreateMap<EditContractCommand, Contracts>()
                .ForMember(dest => dest.TermsAndConditions, opt => opt.MapFrom(src => src.TermsAndConditions))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FreelancerId, opt => opt.Ignore())
                .ForMember(dest => dest.ClientId, opt => opt.Ignore())
                .ForMember(dest => dest.ApplyTaskId, opt => opt.Ignore())
                .ForMember(dest => dest.ContractDate, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore());

            CreateMap<Contracts, ContractDetailsResponse>()
                .ForMember(dest => dest.FreelancerId, opt => opt.MapFrom(src=>src.FreelancerId))
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src=>src.ClientId))
                .ForMember(dest => dest.ClientFullName,opt => opt.MapFrom(src => src.Client.FirstName + " " + src.Client.LastName))
                .ForMember(dest => dest.FreelancerFullName,opt => opt.MapFrom(src => src.Freelancer.FirstName + " " + src.Freelancer.LastName))
                .ForMember(dest => dest.JobPostTitle,opt => opt.MapFrom(src => src.ApplyTask.JobPost.Title))
                .ForMember(dest => dest.JobPostDescription,opt => opt.MapFrom(src => src.ApplyTask.JobPost.Description));

            CreateMap<Contracts, GetMyContractsResponse>()
                .ForMember(dest => dest.FreelancerId, opt => opt.MapFrom(src => src.FreelancerId))
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(dest => dest.ClientFullName, opt => opt.MapFrom(src => src.Client.FirstName + " " + src.Client.LastName))
                .ForMember(dest => dest.FreelancerFullName, opt => opt.MapFrom(src => src.Freelancer.FirstName + " " + src.Freelancer.LastName))
                .ForMember(dest => dest.JobPostTitle, opt => opt.MapFrom(src => src.ApplyTask.JobPost.Title))
                .ForMember(dest => dest.JobPostDescription, opt => opt.MapFrom(src => src.ApplyTask.JobPost.Description));
        }
    }
}
