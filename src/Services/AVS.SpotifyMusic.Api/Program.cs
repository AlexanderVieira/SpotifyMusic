using AVS.SpotifyMusic.Application.AppServices;
using AVS.SpotifyMusic.Application.Contas.AutoMapper;
using AVS.SpotifyMusic.Application.Contas.Interfaces.Services;
using AVS.SpotifyMusic.Application.Contas.Services;
using AVS.SpotifyMusic.Application.Pagamentos.AutoMapper;
using AVS.SpotifyMusic.Application.Streamings.AutoMapper;
using AVS.SpotifyMusic.Domain.Contas.Interfaces.Repositories;
using AVS.SpotifyMusic.Domain.Core.Data;
using AVS.SpotifyMusic.Infra.Data.Context;
using AVS.SpotifyMusic.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

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
			builder.Services.AddSwaggerGen();

			builder.Services.AddDbContext<SpotifyMusicContext>(c =>
			{
				c.UseLazyLoadingProxies();
				c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
			});

			builder.Services.AddScoped<SpotifyMusicContext>();

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

            //Repositories
            builder.Services.AddScoped(typeof(BaseRepository<>));
			builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
			
			//Services
			builder.Services.AddScoped(typeof(BaseService<>));
			builder.Services.AddScoped<IUsuarioService, UsuarioService>();
			builder.Services.AddScoped<UsuarioAppService>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();
            app.UseCors("Total");
            app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
