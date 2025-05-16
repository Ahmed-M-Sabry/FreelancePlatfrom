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
            return services;
        }
    }
}
