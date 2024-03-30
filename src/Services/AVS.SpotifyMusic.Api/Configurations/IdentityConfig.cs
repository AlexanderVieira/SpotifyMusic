using AVS.SpotifyMusic.Api.Data;
using AVS.SpotifyMusic.Api.Extensions;
using AVS.SpotifyMusic.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Identity.Jwt;
using NetDevPack.Security.PasswordHasher.Core;


namespace AVS.SpotifyMusic.Api.Configurations
{
    public static class IdentityConfig
    {
         public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppTokenSettings");
            services.Configure<AppTokenSettings>(appSettingsSection);           

            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                     providerOptions => providerOptions.EnableRetryOnFailure()));          

            services.AddMemoryCache()
                    .AddDataProtection();

            services.AddJwtConfiguration(configuration, "AppSettings")
                    .AddNetDevPackIdentity<IdentityUser>()
                    .PersistKeysToDatabaseStore<AuthDbContext>();

            services.AddIdentity<IdentityUser, IdentityRole>(o =>
                {
                    o.Password.RequireDigit = false;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequireUppercase = false;
                    o.Password.RequiredUniqueChars = 0;
                    o.Password.RequiredLength = 8;
                })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddErrorDescriber<IdentityMessagesPtBr>()
                .AddDefaultTokenProviders();

            services.UpgradePasswordSecurity()
                .WithStrenghten(PasswordHasherStrenght.Moderate)
                .UseArgon2<IdentityUser>();

            return services;
        }
    }
}