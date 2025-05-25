using FluentValidation;
using FreelancePlatform.Core.Features.AuthenticationFeatures.ChangePasswordFeature.Validators;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Models;
using FreelancePlatfrom.Core.Features.ApplyTaskFeatrures.Command.Validator;
using FreelancePlatfrom.Core.Features.AuthenticationFeatures.ChangePasswordFreature.Command.Model;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Models;
using FreelancePlatfrom.Core.Features.ContractsFeatures.Command.Validator;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Command.Models;
using FreelancePlatfrom.Core.Features.jobPostFeatrures.Command.Validator;
using FreelancePlatfrom.Core.Features.JobPostFeatures.Command.Models;
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

            services.AddScoped<IValidator<ChangePasswordCommand>, ChangePasswordValidator>();
            services.AddScoped<IValidator<CreateJobPostCommand>, CreateJobPostValidator>();
            services.AddScoped<IValidator<EditJobPostCommand>, EditJobPostValidator>();
            services.AddScoped<IValidator<CreateApplyTaskCommand>, CreateApplyTaskValidator>();
            services.AddScoped<IValidator<EditApplyTaskCommand>, EditApplyTaskValidator>();

            services.AddScoped<IValidator<CreateContractCommand>, CreateContractCommandValidator>();

            services.AddHttpContextAccessor();



            return services;
        }

    }
}
