using AutoMapper;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.Register.FreelancerRegister.Model;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.RegisterNeeded;
using System.Linq;

namespace FreelancePlatfrom.Core.Mappings
{
    /// <summary>
    /// AutoMapper profile for mapping freelancer-related commands to entities.
    /// </summary>
    public class FreelancerProfile : Profile
    {
        public FreelancerProfile()
        {
            CreateMap<AddFreelancerCommand, ApplicationUser>()
                 .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                 .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                 .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) // Email as UserName
                 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                 .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
                 .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicturePath))
                 .ForMember(dest => dest.YourTitle, opt => opt.MapFrom(src => src.YourTitle))
                 .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                 .ForMember(dest => dest.Education, opt => opt.MapFrom(src => src.Education))
                 .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => src.Experience))
                 .ForMember(dest => dest.HourlyRate, opt => opt.MapFrom(src => src.HourlyRate))
                 .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
                 .ForMember(dest => dest.PortfolioUrl, opt => opt.MapFrom(src => src.PortfolioUrl))
                 .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.State))
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                 .ForMember(dest => dest.Zip, opt => opt.MapFrom(src => src.ZIP))
                 .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => DateTime.UtcNow))
                 .ForMember(dest => dest.LastActive, opt => opt.MapFrom(src => DateTime.UtcNow))
                 .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                 .ForMember(dest => dest.ActiveOrNot, opt => opt.MapFrom(src => "Active"))
                 // Map SelectedLanguages to UserLanguages
                 .ForMember(dest => dest.UserLanguages, opt => opt.MapFrom(src => src.SelectedLanguages != null
                     ? src.SelectedLanguages.Select(lang => new ApplicationUserLanguage { LanguageId = lang }).ToList()
                     : null))
                 // Map SelectedSkills to UserSkills
                 .ForMember(dest => dest.UserSkills, opt => opt.MapFrom(src => src.SelectedSkills != null
                     ? src.SelectedSkills.Select(skillId => new UserSkill { SkillId = skillId }).ToList()
                     : null))
                 // Ignore fields handled elsewhere
                 .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Handled by UserManager
                 .ForMember(dest => dest.JobPosts, opt => opt.Ignore())
                 .ForMember(dest => dest.Portfolios, opt => opt.Ignore())
                 .ForMember(dest => dest.Reviews, opt => opt.Ignore())
                 .ForMember(dest => dest.Reports, opt => opt.Ignore())
                 .ForMember(dest => dest.Contracts, opt => opt.Ignore())
                 .ForMember(dest => dest.ApplyTasksAsFreelancer, opt => opt.Ignore())
                 .ForMember(dest => dest.Favorites, opt => opt.Ignore())
                 .ForMember(dest => dest.FavoritesJobPosts, opt => opt.Ignore())
                 .ForMember(dest => dest.refreshTokens, opt => opt.Ignore());
        }
    }
}