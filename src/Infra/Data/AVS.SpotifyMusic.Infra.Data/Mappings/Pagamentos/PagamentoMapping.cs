using AVS.SpotifyMusic.Domain.Pagamentos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVS.SpotifyMusic.Infra.Data.Mappings.Pagamentos
{
    public class PagamentoMapping : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("Pagamentos");

            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Situacao).IsRequired().HasColumnName("Situacao").HasColumnType("int");

            builder.Property(x => x.DtCriacao).IsRequired().HasColumnName("DataCriacao").HasColumnType("DateTime2");

            builder.Property(x => x.DtAtualizacao).IsRequired(false).HasColumnName("DataAtualizacao").HasColumnType("DateTime2");

            builder.OwnsOne(x => x.Valor, tf =>
            {
                tf.Property(x => x.Valor).IsRequired().HasColumnName("ValorPagamento").HasColumnType("decimal(10, 2)");
            });

            builder.HasOne(x => x.Cartao).WithOne(x => x.Pagamento).HasForeignKey<Cartao>(x => x.PagamentoId);
            builder.HasOne(x => x.Transacao).WithOne(x => x.Pagamento).HasForeignKey<Transacao>(x => x.PagamentoId);
        }
    }
}
