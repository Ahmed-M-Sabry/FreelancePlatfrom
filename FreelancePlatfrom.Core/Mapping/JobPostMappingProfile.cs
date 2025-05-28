using AutoMapper;
using FreelancePlatfrom.Core.Features.JobPostFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Command.Dtos;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Command.Models;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Queries.Result;
using FreelancePlatfrom.Data.Entities.JobPostAndContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancePlatfrom.Core.Features.JopPostFeatrures.Queries.Result;
using FreelancePlatfrom.Core.Features.JopPostFeatrures.Queries.Models;
using FreelancePlatfrom.Core.Features.JobPostFavoriteFeatrures.Queries.Result;
using FreelancePlatfrom.Data.Entities.FavoritesTables;

namespace FreelancePlatfrom.Core.Mapping
{
    public class JobPostMappingProfile : Profile
    {
        public JobPostMappingProfile()
        {
            
            CreateMap<CreateJobPostCommand, JobPost>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.ApplyTasks, opt => opt.Ignore())
                .ForMember(dest => dest.FavJobPosts, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicationUser, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Uncompleted"))
                .ForMember(dest => dest.JobPostSkills, opt => opt.MapFrom(src =>
                    src.SkillIds.Select(id => new JobPostSkill { SkillId = id }).ToList()));

            CreateMap<EditJobPostCommand, JobPost>()
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.ApplyTasks, opt => opt.Ignore())
                .ForMember(dest => dest.FavJobPosts, opt => opt.Ignore())
                .ForMember(dest => dest.ApplicationUser, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "Uncompleted"))
                .ForMember(dest => dest.JobPostSkills, opt => opt.MapFrom(src =>
                    src.SkillIds.Select(id => new JobPostSkill { SkillId = id }).ToList()));

            CreateMap<JobPost, GetMyJobPostDto>()
                .ForMember(dest => dest.SkillIds,opt => opt.MapFrom(src => src.JobPostSkills.Select(s => s.SkillId)))
                .ForMember(dest => dest.SkillNames,opt => opt.MapFrom(src => src.JobPostSkills.Select(s => s.Skill.Name)))
                .ForMember(dest => dest.CategoryName,opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<JobPost, GetJobPostByIdDto>()
                .ForMember(dest => dest.SkillIds, opt => opt.MapFrom(src => src.JobPostSkills.Select(s => s.SkillId)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ApplicationUser.FirstName + " " + src.ApplicationUser.LastName))
                .ForMember(dest => dest.SkillNames, opt => opt.MapFrom(src => src.JobPostSkills.Select(s => s.Skill.Name)))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<JobPost, GetAllJobPostDto>()
                .ForMember(dest => dest.SkillIds, opt => opt.MapFrom(src => src.JobPostSkills.Select(s => s.SkillId)))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ApplicationUser.FirstName+" "+src.ApplicationUser.LastName))
                .ForMember(dest => dest.SkillNames, opt => opt.MapFrom(src => src.JobPostSkills.Select(s => s.Skill.Name)))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));



        }
    }
}
