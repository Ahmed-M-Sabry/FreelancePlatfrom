using FreelancePlatfrom.Data.Entities.Identity;
using FreelancePlatfrom.Data.Entities.Identity.Helper;
using FreelancePlatfrom.infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FreelancePlatfrom.infrastructure
{
    public static class ModelDependenciesAuthenticationServices
    {
        public static IServiceCollection AuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // 1. Bind JWT settings
            var jwtSettings = new JwtSittings();
            configuration.GetSection("jwtSittings").Bind(jwtSettings);
            services.AddSingleton(jwtSettings);

            // 2. Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // 3. JWT Auth
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });

            

            return services;
        }

    }
}
