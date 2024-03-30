using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using NetDevPack.Security.JwtExtensions;

namespace AVS.SpotifyMusic.Api.Configurations
{
    public static class JwtConfig
    {
         public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<JwkOptions>(appSettingsSection);

            var jwkOptions = appSettingsSection.Get<JwkOptions>();
            jwkOptions.KeepFor = TimeSpan.FromMinutes(15);
            if(Debugger.IsAttached)
                IdentityModelEventSource.ShowPII = true;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.BackchannelHttpHandler = new HttpClientHandler 
                { 
                    ServerCertificateCustomValidationCallback = delegate { return true; }
                };
                options.SaveToken = true;
                options.SetJwksOptions(jwkOptions);
            });

            services.AddAuthorization();
        }

        public static void UseAuthConfiguration(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}