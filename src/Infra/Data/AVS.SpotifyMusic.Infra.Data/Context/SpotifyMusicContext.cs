using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Core.Data;
using AVS.SpotifyMusic.Domain.Core.Notificacoes;
using AVS.SpotifyMusic.Domain.Pagamentos.Entidades;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AVS.SpotifyMusic.Infra.Data.Context
{
    public class SpotifyMusicContext : DbContext, IUnitOfWork
    {
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
        public IConfigurationRoot Configuration { get; set; }

        public SpotifyMusicContext(DbContextOptions<SpotifyMusicContext> options) : base(options)
        {
            Database.Migrate();    
            ChangeTracker.LazyLoadingEnabled = false;            
        }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpotifyMusicContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCriacao") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCriacao").CurrentValue = DateTime.Now;                    
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCriacao").IsModified = false;
                    entry.Property("DataAtualizacao").CurrentValue = DateTime.Now;
                }
            }

            var sucesso = await base.SaveChangesAsync() > 0;            

            return sucesso;
        }
    }

    public class SpotifyMusicDbContextFactory : IDesignTimeDbContextFactory<SpotifyMusicContext>
    {
        public SpotifyMusicContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SpotifyMusicContext>();
            return new SpotifyMusicContext(optionsBuilder.Options);
        }
    }
}
