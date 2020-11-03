using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SEBtask.Clients;
using SEBtask.Repositories;
using SEBtask.Services;
using System.Text;

namespace SEBtask.Base.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDependacies(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IAgreementRepository, AgreementRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IInterestRateService, InterestRateService>();
            services.AddScoped<IViliborService, ViliborService>();
            services.AddScoped<IViliborClient, ViliborClient>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICacheService, CacheService>();
            services.AddScoped<IResponseBuildService, ResponseBuildService>();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", 
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .Build());
            });
        }

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<RepositoryContext>(opt =>
                opt.UseSqlServer(config.GetConnectionString("DefaultConnection")));
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = CreateTokenValidationParameters(config);
                });
        }

        private static TokenValidationParameters CreateTokenValidationParameters(IConfiguration config)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
            };
        }
    }
}
