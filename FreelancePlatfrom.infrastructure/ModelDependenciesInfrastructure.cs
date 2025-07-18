﻿using FreelancePlatform.Infrastructure.RepositoryImplemention;
using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.infrastructure.BaseRepository;
using FreelancePlatfrom.infrastructure.Data;
using FreelancePlatfrom.infrastructure.IRepositoryAbstraction;
using FreelancePlatfrom.infrastructure.RepositoryImplemention;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace FreelancePlatfrom.infrastructure
{
    public static class ModelDependenciesInfrastructure
    {
        public static IServiceCollection ModelDependenciesInfrastructureServices(this IServiceCollection services)
        {

            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection")));

            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IUserSkillsRepository, UserSkillsRepository>();
            services.AddTransient<ILanguagesRepository, LanguagesRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IjobPostRepository, jobPostRepository>();
            services.AddTransient<IJobPostSkillServicesRepository, JobPostSkillServicesRepository>();
            services.AddTransient<ISkillRepostitory, SkillRepostitory>();
            services.AddTransient<IApplyTaskRepository, ApplyTaskRepository>();
            services.AddTransient<IFavoritesFreelancerRepository, FavoritesFreelancerRepository>();
            services.AddTransient<IJobPostFavouriteRepository, JobPostFavouriteRepository>();
            services.AddTransient<IContractRepository, ContractRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();
            services.AddTransient<IRepostRepostiory, RepostRepostiory>();
            services.AddTransient<IProfileRespository, ProfileRespository>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            return services;
        }
    }
}
