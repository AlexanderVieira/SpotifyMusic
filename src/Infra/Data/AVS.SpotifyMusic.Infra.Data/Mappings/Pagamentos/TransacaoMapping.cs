using AVS.SpotifyMusic.Domain.Pagamentos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVS.SpotifyMusic.Infra.Data.Mappings.Pagamentos
{
    public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transacoes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Situacao).IsRequired().HasColumnName("Situacao").HasColumnType("int");

            builder.Property(x => x.Descricao).IsRequired(false).HasColumnName("Descricao").HasColumnType("varchar(500)").HasMaxLength(500);

            builder.Property(x => x.DtCriacao).IsRequired().HasColumnName("DataTransacao").HasColumnType("DateTime2");

            builder.Property(x => x.DtAtualizacao).IsRequired(false).HasColumnName("DataAtualizacao").HasColumnType("DateTime2");

            builder.OwnsOne(x => x.Valor, tf =>
            {
                tf.Property(x => x.Valor).IsRequired().HasColumnName("Valor").HasColumnType("decimal(10,2)");
            });

            builder.OwnsOne(x => x.Merchant, tf =>
            {
                tf.Property(x => x.Nome).IsRequired().HasColumnName("NomeMerchant").HasColumnType("varchar(50)");
            });
        }
    }
}
