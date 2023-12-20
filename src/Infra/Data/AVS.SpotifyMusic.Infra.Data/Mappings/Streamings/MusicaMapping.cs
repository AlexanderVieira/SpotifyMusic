using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVS.SpotifyMusic.Infra.Data.Mappings.Streamings
{
    public class MusicaMapping : IEntityTypeConfiguration<Musica>
    {
        public void Configure(EntityTypeBuilder<Musica> builder)
        {
            builder.ToTable("Musicas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome).IsRequired().HasColumnName("Nome").HasColumnType("varchar(150)").HasMaxLength(150);

            builder.Property(x => x.DtCriacao).IsRequired().HasColumnName("DataCriacao").HasColumnType("DateTime2");

            builder.Property(x => x.DtAtualizacao).IsRequired(false).HasColumnName("DataAtualizacao").HasColumnType("DateTime2");

            builder.OwnsOne(x => x.Duracao, tf =>
            {
                tf.Property(x => x.Valor).IsRequired().HasColumnName("Duracao").HasColumnType("int");
            });
        }
    }
}
