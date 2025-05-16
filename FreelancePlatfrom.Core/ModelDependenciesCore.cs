using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FreelancePlatfrom.Core
{
    public static class ModelDependenciesCore
    {
        public static IServiceCollection ModelDependenciesCoreService(this IServiceCollection services)
        {
            // Add your service descriptors here
            // Example: services.AddScoped<IYourService, YourService>();

            services.AddMediatR(c => c.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddHttpContextAccessor();



            return services;
        }

    }
}
