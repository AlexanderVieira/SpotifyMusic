using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVS.SpotifyMusic.Infra.Data.Mappings.Streamings
{
    public class AlbumMapping : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Albuns");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Titulo).IsRequired().HasColumnName("Titulo").HasColumnType("varchar(150)").HasMaxLength(150);

            builder.Property(x => x.Descricao).IsRequired().HasColumnName("Descricao").HasColumnType("varchar(500)").HasMaxLength(500);

            builder.Property(x => x.Foto).IsRequired(false).HasColumnName("Foto").HasColumnType("varchar(200)").HasMaxLength(250);

            builder.Property(x => x.DtCriacao).IsRequired().HasColumnName("DataCriacao").HasColumnType("DateTime2");

            builder.Property(x => x.DtAtualizacao).IsRequired(false).HasColumnName("DataAtualizacao").HasColumnType("DateTime2");

            builder.HasMany(x => x.Musicas).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
