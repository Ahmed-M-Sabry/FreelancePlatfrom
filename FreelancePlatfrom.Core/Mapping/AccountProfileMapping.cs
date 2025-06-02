using AutoMapper;
using FreelancePlatfrom.Core.Features.AccountFeatures.Queries.Results;
using FreelancePlatfrom.Data.Entities.Identity;
using System.Linq;

namespace FreelancePlatfrom.Core.Mapping
{
    public class AccountProfileMapping : Profile
    {
        public AccountProfileMapping()
        {
            CreateMap<ApplicationUser, FreelancerProfileResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture))
                .ForMember(dest => dest.YourTitle, opt => opt.MapFrom(src => src.YourTitle))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.Education))
                .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience))
                .ForMember(dest => dest.HourlyRate, opt => opt.MapFrom(src => src.HourlyRate))
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.Zip))
                .ForMember(dest => dest.PortfolioUrl, opt => opt.MapFrom(src => src.PortfolioUrl))
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.country != null ? src.country.Name : string.Empty))
                .ForMember(dest => dest.LanguageName, opt => opt.MapFrom(src => src.UserLanguages != null
                        ? src.UserLanguages.Select(l => l.Language.Value ?? string.Empty).ToList()
                        : new List<string>()))
                .ForMember(dest => dest.SkillName, opt => opt.MapFrom(src => src.UserSkills != null
                        ? src.UserSkills.Select(s => s.Skill.Name ?? string.Empty).ToList()
                        : new List<string>()))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src =>
                        (src.Reviews != null && src.Reviews.Any())? src.Reviews.Average(r => r.Rate)  
                            : 0));
            CreateMap<ApplicationUser, GetUserProfileResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.country != null ? src.country.Name : string.Empty));

            CreateMap<ApplicationUser, SearchFreelancerResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture));
        }
    }
}
