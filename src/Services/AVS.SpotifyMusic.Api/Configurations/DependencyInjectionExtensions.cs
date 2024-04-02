using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AVS.SpotifyMusic.Api.Configurations
{
    public static class DependencyInjectionExtensions
    {
        public static void ScanDependencyInjection(this IServiceCollection services, Assembly projectAssembly, string classEndWith){

            var types = projectAssembly.GetTypes().Where(x => x.GetInterfaces().Any(i => i.Name.EndsWith(classEndWith)));
            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces();
                foreach (var inter in interfaces)
                {
                    services.AddScoped(inter, type);
                }
            }

        }
        
    }
}