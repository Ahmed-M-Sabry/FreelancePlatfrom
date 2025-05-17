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

            services.AddTransient<ISkillService, SkillService>();
            services.AddTransient<ILanguageService, LanguageService>();

            services.AddTransient<IFileService, FileService>();

            return services;
        }
    }
}
