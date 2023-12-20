using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVS.SpotifyMusic.Infra.Data.Mappings.Streamings
{
    public class PlanoMapping : IEntityTypeConfiguration<Plano>
    {
        public void Configure(EntityTypeBuilder<Plano> builder)
        {
            builder.ToTable("Planos");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome).IsRequired().HasColumnName("Nome").HasColumnType("varchar(150)").HasMaxLength(150);

            builder.Property(x => x.Descricao).IsRequired().HasColumnName("Descricao").HasColumnType("varchar(500)").HasMaxLength(500);

            builder.Property(x => x.TipoPlano).IsRequired().HasColumnName("TipoPlano").HasColumnType("int");

            builder.Property(x => x.DtCriacao).IsRequired().HasColumnName("DataCriacao").HasColumnType("DateTime2");

            builder.Property(x => x.DtAtualizacao).IsRequired(false).HasColumnName("DataAtualizacao").HasColumnType("DateTime2");

            builder.OwnsOne(x => x.Valor, tf =>
            {
                tf.Property(x => x.Valor).IsRequired().HasColumnName("ValorPlano").HasColumnType("decimal(10, 2)");
            });
            
        }
    }
}
