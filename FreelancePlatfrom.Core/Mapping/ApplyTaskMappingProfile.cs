using AutoMapper;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Dtos;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Models;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Queries.Result;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Mapping
{
    public class ApplyTaskMappingProfile : Profile
    {
        public ApplyTaskMappingProfile()
        {
            CreateMap<CreateApplyTaskCommand, ApplyTask>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Pending"))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.JobPostId, opt => opt.MapFrom(src => src.JobPostId))
                .ForMember(dest => dest.FreelancerId, opt => opt.MapFrom(src => src.FreelancerId))
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId));

            CreateMap<EditApplyTaskCommand, ApplyTask>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Pending"))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(dest => dest.JobPostId, opt => opt.MapFrom(src => src.JobPostId))
                .ForMember(dest => dest.FreelancerId, opt => opt.MapFrom(src => src.FreelancerId))
                .ForMember(dest => dest.ClientId, opt => opt.MapFrom(src => src.ClientId));

            CreateMap<ApplyTask, GetFreelancerApplyTaskDto>()
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.OfferDescription, opt => opt.MapFrom(src => src.OfferDescription))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.JobPostId, opt => opt.MapFrom(src => src.JobPostId))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.ClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.ClientName, opt => opt.MapFrom(src => src.Freelancer.FirstName + " " + src.Freelancer.LastName))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.JobPostName, opt => opt.MapFrom(src => src.JobPost.Title))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.JobPostDescription, opt => opt.MapFrom(src => src.JobPost.Description));

            CreateMap<ApplyTask, GetClientApplyTaskDto>()
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.OfferDescription, opt => opt.MapFrom(src => src.OfferDescription))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.JobPostId, opt => opt.MapFrom(src => src.JobPostId))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.FreeLanceId, opt => opt.MapFrom(src => src.FreelancerId))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.FreeLancerName, opt => opt.MapFrom(src => src.Freelancer.FirstName + " " + src.Freelancer.LastName))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.JobPostName, opt => opt.MapFrom(src => src.JobPost.Title))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.JobPostDescription, opt => opt.MapFrom(src => src.JobPost.Description));

            CreateMap<ApplyTask, GetApplyTaskByIdDto>()
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.OfferDescription, opt => opt.MapFrom(src => src.OfferDescription))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.JobPostId, opt => opt.MapFrom(src => src.JobPostId))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.FreeLanceId, opt => opt.MapFrom(src => src.FreelancerId))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.FreeLancerName, opt => opt.MapFrom(src => src.Freelancer.FirstName + " " + src.Freelancer.LastName))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.JobPostName, opt => opt.MapFrom(src => src.JobPost.Title))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.JobPostDescription, opt => opt.MapFrom(src => src.JobPost.Description))
                .ForMember(GetApplyTaskByIdDto => GetApplyTaskByIdDto.ClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(GetApplyTaskByIdDto => GetApplyTaskByIdDto.ClientName, opt => opt.MapFrom(src => src.Client.FirstName + " " + src.Client.LastName));

            CreateMap<ApplyTask, GetApplyTaskByJobPostIdDto>()
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.OfferDescription, opt => opt.MapFrom(src => src.OfferDescription))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.DeliveryDate, opt => opt.MapFrom(src => src.DeliveryDate))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.JobPostId, opt => opt.MapFrom(src => src.JobPostId))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.FreeLanceId, opt => opt.MapFrom(src => src.FreelancerId))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.FreeLancerName, opt => opt.MapFrom(src => src.Freelancer.FirstName + " " + src.Freelancer.LastName))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.JobPostName, opt => opt.MapFrom(src => src.JobPost.Title))
                .ForMember(GetMyApplyTaskDto => GetMyApplyTaskDto.JobPostDescription, opt => opt.MapFrom(src => src.JobPost.Description))
                .ForMember(GetApplyTaskByIdDto => GetApplyTaskByIdDto.ClientId, opt => opt.MapFrom(src => src.ClientId))
                .ForMember(GetApplyTaskByIdDto => GetApplyTaskByIdDto.ClientName, opt => opt.MapFrom(src => src.Client.FirstName + " " + src.Client.LastName));
        }
    }
}
