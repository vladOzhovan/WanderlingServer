using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Wanderling.Application.Interfaces;
using Wanderling.Application.Services;
using Wanderling.Domain.Interfaces;
using Wanderling.Domain.Reproduction;
using Wanderling.Infrastructure.Data;
using Wanderling.Infrastructure.Entities;
using Wanderling.Infrastructure.Factories;
using Wanderling.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Wanderling.Infrastructure.Services;


namespace Wanderling.Api.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                           IConfiguration configuration, IWebHostEnvironment env)
        { 
            var dbPath = Path.Combine(env.ContentRootPath, "..", "Wanderling.Infrastructure", "Wanderlings.db");
            var connectionString = $"Data Source={dbPath}";

            services.AddMemoryCache();
            services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

            services.AddIdentity<UserEntity, IdentityRole<Guid>>(options =>
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
                        System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"])),
                    RoleClaimType = ClaimTypes.Role
                };
            });

            services.AddScoped<IReproduction, SeedReproduction>();
            services.AddScoped<IReproduction, SporeReproduction>();
            services.AddScoped<IPlantRepository, PlantRepository>();
            services.AddScoped<IReproduction, VegetativeReproduction>();
            services.AddScoped<IPlantCreationService, PlantCreationService>();
            services.AddScoped<IDiscoveredPlantCreationService, DiscoveredPlantCreationService>();
            services.AddScoped<ITokenService, TokenService>();

            // bind configuration section to options
            services.Configure<PlantRecognitionOptions>(configuration.GetSection("PlantRecognition"));

            // register HttpClient factory
            services.AddHttpClient();

            // register IPlantRecognitionService using a factory
            services.AddTransient<IPlantRecognitionService>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<PlantRecognitionOptions>>().Value;
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient();
                return new PlantRecognitionService(httpClient, options.ApiKey, options.ApiUrl);

            });

            var resourcePath = Path.Combine(env.ContentRootPath, "..", "Wanderling.Infrastructure", "Resources", "plantsRegister.json");

            services.AddSingleton<IOrganismFactory>(sp =>
            {
                return new PlantFactory(resourcePath);
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SupportNonNullableReferenceTypes();
            });

            return services;
        }
    }
}
