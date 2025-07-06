using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Wanderling.Domain.Interfaces;
using Wanderling.Domain.Strategies;
using Wanderling.Infrastructure.Configuration;
using Wanderling.Infrastructure.Data;
using Wanderling.Infrastructure.Entities;
using Wanderling.Infrastructure.Factories;
using Wanderling.Infrastructure.Startup;

namespace Wanderling.Api.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqliteConnection");

            services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

            services.AddIdentity<UserEntity, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    //ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                    RoleClaimType = ClaimTypes.Role
                };
            });

            services.AddSingleton<IOrganismFactory, DynamicOrganismFactory>(serviceProvider =>
            {
                var configs = OrganismConfigLoader.LoadFromFile("Resources/organisms.json");
                return new DynamicOrganismFactory(configs, serviceProvider);
            });

            services.AddSingleton<OrganismRegistryBuilder>(sp =>
            {
                var configs = OrganismConfigLoader.LoadFromFile("Resources/organisms.json");
                return new OrganismRegistryBuilder(configs);
            });

            services.AddTransient<IReproductionStrategy, SeedReproduction>();
            services.AddTransient<IReproductionStrategy, SexualReproduction>();
            services.AddTransient<IReproductionStrategy, SporeReproduction>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
