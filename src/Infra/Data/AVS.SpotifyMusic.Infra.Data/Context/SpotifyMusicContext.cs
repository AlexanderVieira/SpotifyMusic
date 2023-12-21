using AVS.SpotifyMusic.Domain.Conta.Entidades;
using AVS.SpotifyMusic.Domain.Core.Notificacoes;
using AVS.SpotifyMusic.Domain.Pagamentos.Entidades;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AVS.SpotifyMusic.Infra.Data.Context
{
    public class SpotifyMusicContext : DbContext
    {
        //private readonly IHostingEnvironment _env;

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Assinatura> Assinaturas { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Notificacao> Notificacoes { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Cartao> Cartoes { get; set; }
        public DbSet<Transacao> Transacao { get; set; }
        public DbSet<Banda> Bandas { get; set; }
        public DbSet<Album> Albuns { get; set; }
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Plano> Planos { get; set; }
        //public IConfigurationRoot Configuration { get; set; }

        public SpotifyMusicContext(DbContextOptions<SpotifyMusicContext> options) : base(options)
        {
            Database.Migrate();            
            //_env = env;
        }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpotifyMusicContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", true, true)
            //    //.AddJsonFile($"appsettings.{_env.EnvironmentName}.json", true, true)
            //    .AddEnvironmentVariables();

            //Configuration = builder.Build();
            
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(x => x.AddConsole()));
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Desenv\\DataSources\\LocalDB\\spotifymusic.mdf;Initial Catalog=spotifymusic;Integrated Security=True;Connect Timeout=30;");
            //optionsBuilder.UseSqlServer("Data Source=DESKTOP-GCGB03L\\SQLEXPRESS;Initial Catalog=spotifymusic;Integrated Security=True");
            
            base.OnConfiguring(optionsBuilder);
        }
    }

    public class SpotifyMusicDbContextFactory : IDesignTimeDbContextFactory<SpotifyMusicContext>
    {
        public SpotifyMusicContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SpotifyMusicContext>();
            //optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\Desenv\\DataSources\\LocalDB\\spotifymusic.mdf;Initial Catalog=spotifymusic;Integrated Security=True;Connect Timeout=30;");

            return new SpotifyMusicContext(optionsBuilder.Options);
        }
    }
}
