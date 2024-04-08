using AVS.SpotifyMusic.Api.Configurations;
using AVS.SpotifyMusic.Api.Services;
using AVS.SpotifyMusic.Api.Services.Interfaces;
using AVS.SpotifyMusic.Application.AppServices;
using AVS.SpotifyMusic.Application.Contas.AutoMapper;
using AVS.SpotifyMusic.Application.Contas.Interfaces.Services;
using AVS.SpotifyMusic.Application.Contas.Services;
using AVS.SpotifyMusic.Application.Pagamentos.AutoMapper;
using AVS.SpotifyMusic.Application.Streamings.AutoMapper;
using AVS.SpotifyMusic.Domain.Contas.Interfaces.Repositories;
using AVS.SpotifyMusic.Domain.Core.Data;
using AVS.SpotifyMusic.Domain.Core.Services.WebApi.AspNetUser;
using AVS.SpotifyMusic.Domain.Core.Services.WebApi.AspNetUser.Interfaces;
using AVS.SpotifyMusic.Domain.Streaming.Interfaces.Repositories;
using AVS.SpotifyMusic.Infra.Data.Context;
using AVS.SpotifyMusic.Infra.Data.Repositories;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using NetDevPack.Security.JwtSigningCredentials.AspNetCore;
using System.Globalization;

namespace AVS.SpotifyMusic.Api
{
    public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo 
				{
                    //Title = "SpotityMusic.API", Version = "v1" 
                    Title = "SpotityMusic API",
                    Description = "Esta API faz parte do estudo da tecnologia .NET.",
                    Contact = new OpenApiContact() { Name = "Alexander Silva", Email = "asilva943rj@gmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/Licenses/MIT") }
                });
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = @"Insira o token JWT desta maneira: Bearer {seu token}. Exemplo: 'Bearer 12345abcdef'",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT"
                });

				options.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							//Scheme = "oauth2",
							//Name = "Bearer",
							//In = ParameterLocation.Header
						},
						new string[]{ }
					}
				});
			});

			builder.Services.AddDbContext<SpotifyMusicContext>(c =>
			{
				c.UseLazyLoadingProxies();
				c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                //c.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            });

			builder.Services.AddScoped<SpotifyMusicContext>();

			builder.Services.AddIdentityConfiguration(builder.Configuration);

			builder.Services.AddAutoMapper(typeof(ContasMappingProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(PagamentosMappingProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(StreamingsMappingProfile).Assembly);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

			//HttpContext
			builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			//User
            builder.Services.AddScoped<IAspNetUser, AspNetUser>();            

            //Repositories
            builder.Services.AddScoped(typeof(BaseRepository<>));
			builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<IBandaRepository, BandaRepository>();

            //Services
            builder.Services.AddScoped(typeof(BaseService<>));
			builder.Services.AddScoped<IUsuarioService, UsuarioService>();
			builder.Services.AddScoped<UsuarioAppService>();

            builder.Services.AddScoped<IBandaService, BandaService>();
            builder.Services.AddScoped<BandaAppService>();

			builder.Services.AddScoped<IUploadService, UploadService>();

			//builder.Services.ScanDependencyInjection(Assembly.GetExecutingAssembly(), "Service");
			
			builder.Services.AddScoped<AuthService>();

            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));
			}

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseHttpsRedirection();
			app.UseRouting();
            app.UseCors("Total");
            app.UseAuthConfiguration();
			app.UseJwksDiscovery();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.MapControllers();
			app.Run();
		}
	}
}
