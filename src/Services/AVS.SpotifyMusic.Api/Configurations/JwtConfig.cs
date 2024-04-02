using AVS.SpotifyMusic.Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NetDevPack.Security.JwtExtensions;

namespace AVS.SpotifyMusic.Api.Configurations
{
    public static class JwtConfig
    {
        public static void AddJwtConfiguration(this IServiceCollection services,
           IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.BackchannelHttpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = delegate { return true; } };
                x.SaveToken = true;
                x.SetJwksOptions(new JwkOptions(appSettings.AuthenticationJwksUrl));
            });
        }

        //public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var appSettingsSection = configuration.GetSection("AppSettings");
        //    services.Configure<JwkOptions>(appSettingsSection);

        //    var jwkOptions = appSettingsSection.Get<JwkOptions>();
        //    jwkOptions.KeepFor = TimeSpan.FromMinutes(15);
        //    if(Debugger.IsAttached)
        //        IdentityModelEventSource.ShowPII = true;

        //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        //    {
        //        options.RequireHttpsMetadata = false;
        //        options.BackchannelHttpHandler = new HttpClientHandler 
        //        { 
        //            ServerCertificateCustomValidationCallback = delegate { return true; }
        //        };
        //        options.SaveToken = true;
        //        options.SetJwksOptions(jwkOptions);
        //    });

        //    services.AddAuthorization();
        //    services.AddJwksManager().UseJwtValidation();
        //    return services;
        //}

        public static void UseAuthConfiguration(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}