using ColorPaletteGeneratorApi.Data;
using ColorPaletteGeneratorApi.Data.Repositories.Implementations;
using ColorPaletteGeneratorApi.Data.Repositories.Interfaces;
using ColorPaletteGeneratorApi.Models;
using ColorPaletteGeneratorApi.Services.Implementations;
using ColorPaletteGeneratorApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ColorPaletteGeneratorApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddCors(opt =>
            {
                opt.AddPolicy(Constants.DEFAULT_CORS_POLICY, policy => policy.WithOrigins(configuration["AllowedCorsOrigins"]).AllowAnyMethod().AllowAnyHeader());
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtSettings:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    ClockSkew = TimeSpan.FromSeconds(0)
                };
            });

            services.AddAuthorization();

            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(Program));

            AddBusinessServices(services);
            AddRepositories(services);
        }

        private static void AddBusinessServices(IServiceCollection services)
        {
            services.AddScoped<IAppUsersServices, AppUsersServices>();
            services.AddScoped<IAuthenticationTokenService, AuthenticationTokenService>();
            services.AddScoped<IHashingService, HashingService>();
            services.AddScoped<IPalettesServices, PalettesServices>();
            services.AddScoped<IPaletteGeneratorServices, PaletteGeneratorServices>();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IAppUsersRepository, AppUsersRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPalettesRepository, PalettesRepository>();
        }
    }
}
