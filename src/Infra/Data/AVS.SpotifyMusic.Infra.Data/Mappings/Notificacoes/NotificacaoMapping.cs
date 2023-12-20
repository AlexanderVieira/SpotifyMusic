using AVS.SpotifyMusic.Domain.Core.Notificacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVS.SpotifyMusic.Infra.Data.Mappings.Notificacoes
{
    public class NotificacaoMapping : IEntityTypeConfiguration<Notificacao>
    {
        public void Configure(EntityTypeBuilder<Notificacao> builder)
        {
            builder.ToTable("Notificacoes");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Titulo).IsRequired().HasColumnName("Titulo").HasColumnType("varchar(150)").HasMaxLength(150);

            builder.Property(x => x.Mensagem).IsRequired().HasColumnName("Mensagem").HasColumnType("varchar(250)").HasMaxLength(250);

            builder.Property(x => x.DtCriacao).IsRequired().HasColumnName("DataNotificacao").HasColumnType("DateTime2");

            builder.Property(x => x.DtAtualizacao).IsRequired(false).HasColumnName("DataAtualizacao").HasColumnType("DateTime2");

            builder.Property(x => x.TipoNotificacao).IsRequired().HasColumnName("TipoNotificacao").HasColumnType("int");

            builder.HasOne(x => x.Destino).WithMany(x => x.Notificacoes).IsRequired().OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Remetente).WithMany().IsRequired(false);
        }
    }
}
