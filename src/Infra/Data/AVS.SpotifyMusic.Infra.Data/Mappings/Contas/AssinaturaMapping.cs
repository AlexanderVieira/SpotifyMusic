using AVS.SpotifyMusic.Domain.Conta.Entidades;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVS.SpotifyMusic.Infra.Data.Mappings.Contas
{
    public class AssinaturaMapping : IEntityTypeConfiguration<Assinatura>
    {
        public void Configure(EntityTypeBuilder<Assinatura> builder)
        {
            builder.ToTable("Assinaturas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Ativo).IsRequired().HasColumnName("Ativo").HasColumnType("Bit");            

            builder.Property(x => x.DtCriacao).IsRequired().HasColumnName("DataCriacao").HasColumnType("DateTime2");

            builder.Property(x => x.DtAtualizacao).HasColumnName("DataAtualizacao").HasColumnType("DateTime2");

            builder.HasOne(x => x.Plano).WithOne(y => y.Assinatura).HasForeignKey<Plano>(x => x.AssinaturaId);
           
        }
    }
}
 