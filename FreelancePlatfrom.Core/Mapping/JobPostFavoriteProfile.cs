using AutoMapper;
using FreelancePlatfrom.Core.Features.JobPostFavoriteFeatrures.Queries.Result;
using FreelancePlatfrom.Data.Entities.FavoritesTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreelancePlatfrom.Core.Mapping
{
    public class JobPostFavoriteProfile : Profile
    {
        public JobPostFavoriteProfile()
        {
            CreateMap<FavJobPost, FavJobPostResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.JobPost.Id))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.JobPost.Title))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.JobPost.Description));
        }
    }
}
