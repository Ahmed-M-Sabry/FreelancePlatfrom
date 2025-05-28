using AutoMapper;
using FreelancePlatfrom.Core.Features.FavoritesFreelancerFeatures.Queries.Results;
using FreelancePlatfrom.Data.Entities.FavoritesTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Mapping
{
    internal class FavoritesFreelancerMappingProfile : Profile
    {
        public FavoritesFreelancerMappingProfile()
        {
            CreateMap<FavoritesFreelancer, GetMyFavoritesFreelancerResponse>()
                .ForMember(dest => dest.FreelancerId, opt => opt.MapFrom(src => src.FreelancerId))
                .ForMember(dest => dest.FreelancerName, opt => opt.MapFrom(src => src.Freelancer.FirstName + " " + src.Freelancer.LastName))
                .ForMember(dest => dest.FreelancerImage, opt => opt.MapFrom(src => src.Freelancer.ProfilePicture));
        }
    }

}
