using AutoMapper;
using FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.ReviewFeatures.Command.Queries.Results;
using FreelancePlatfrom.Data.Entities.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Mapping
{
    public class ReviewMappingProfile : Profile
    {
        public ReviewMappingProfile()
        {

            CreateMap<CreateReviewCommand, Review>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.RateDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.ClientId, opt => opt.Ignore());

            CreateMap<EditReviewCommand, Review>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ClientId, opt => opt.Ignore())
                .ForMember(dest => dest.FreelancerId, opt => opt.Ignore())
                .ForMember(dest => dest.ContractId, opt => opt.Ignore())
                .ForMember(dest => dest.RateDate, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore());

            CreateMap<Review, GetReviewByIdResponse>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.FirstName + " " + src.Client.LastName))
                .ForMember(dest => dest.FreelancerName, opt => opt.MapFrom(src => src.Freelancer.FirstName + " " + src.Freelancer.LastName));

            CreateMap<Review, GetReviewResponse>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.FirstName +" "+ src.Client.LastName))
                .ForMember(dest => dest.FreelancerName, opt => opt.MapFrom(src => src.Freelancer.FirstName + " " + src.Freelancer.LastName));

        }
    }
}
