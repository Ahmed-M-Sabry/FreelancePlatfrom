﻿using FreelancePlatform.Infrastructure.Services;
using FreelancePlatform.Service.AbstractionServices;
using FreelancePlatform.Service.ImplementationServices;
using FreelancePlatfrom.Service.AbstractionServices;
using FreelancePlatfrom.Service.ImplementationServices;
using Microsoft.Extensions.DependencyInjection;

namespace FreelancePlatfrom.Service
{
    public static class ModelDependenciesService
    {
        public static IServiceCollection ModelDependenciesServiceService(this IServiceCollection services)
        {
            // Add your service descriptors here
            // Example: services.AddScoped<IYourService, YourService>();

            services.AddTransient<ICountryServices, CountryServices>();
            services.AddTransient<IAuthenticatioService, AuthenticatioService>();
            services.AddTransient<IUserSkillesService, UserSkillesService>();
            services.AddTransient<ILanguagesService, LanguagesService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IjobPostServices, jobPostServices>();
            services.AddTransient<IJobPostSkillServices, JobPostSkillServices>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<ISkillService, SkillService>();
            services.AddTransient<IApplyTaskService, ApplyTaskService>();
            services.AddTransient<IFavoritesFreelancerServices, FavoritesFreelancerServices>();
            services.AddTransient<IFavoriteJobPostService , FavoriteJobPostService>();
            services.AddTransient<IContractService, ContractService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IReportService  , ReportService>();
            services.AddTransient<IPortfolioService, PortfolioService>();
            services.AddTransient<IAccountService, AccountService>();

            return services;
        }
    }
}
