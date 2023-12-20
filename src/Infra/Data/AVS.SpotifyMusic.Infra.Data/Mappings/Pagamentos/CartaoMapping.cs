using AVS.SpotifyMusic.Domain.Pagamentos.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVS.SpotifyMusic.Infra.Data.Mappings.Pagamentos
{
    public class CartaoMapping : IEntityTypeConfiguration<Cartao>
    {
        public void Configure(EntityTypeBuilder<Cartao> builder)
        {
            builder.ToTable("Cartoes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome).IsRequired().HasColumnName("Nome").HasColumnType("varchar(30)").HasMaxLength(30);

            builder.Property(x => x.Numero).IsRequired().HasColumnName("Numero").HasColumnType("varchar(19)").HasMaxLength(19);

            builder.Property(x => x.Expiracao).IsRequired().HasColumnName("DataExpiracao").HasColumnType("varchar(7)").HasMaxLength(7);

            builder.Property(x => x.Cvv).IsRequired().HasColumnName("Cvv").HasColumnType("varchar(3)").HasMaxLength(3);

            builder.Property(x => x.Ativo).IsRequired().HasColumnName("Ativo").HasColumnType("Bit");

            builder.Property(x => x.DtCriacao).IsRequired().HasColumnName("DataCriacao").HasColumnType("DateTime2");

            builder.Property(x => x.DtAtualizacao).IsRequired(false).HasColumnName("DataAtualizacao").HasColumnType("DateTime2");

            builder.OwnsOne(x => x.Limite, tf =>
            {
                tf.Property(x => x.Valor).IsRequired().HasColumnName("Limite").HasColumnType("decimal(10, 2)");
            });

            builder.HasMany(x => x.Transacoes).WithOne();
        }
    }
}
