using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Core.ObjValor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVS.SpotifyMusic.Infra.Data.Mappings.Contas
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome).IsRequired().HasColumnName("Nome").HasColumnType("varchar(150)").HasMaxLength(150);

            builder.Property(x => x.Foto).IsRequired(false).HasColumnName("Foto").HasColumnType("varchar(200)").HasMaxLength(250);

            builder.Property(x => x.Ativo).IsRequired().HasColumnName("Ativo").HasColumnType("Bit");

            builder.Property(x => x.DtNascimento).IsRequired().HasColumnName("DataNascimento").HasColumnType("DateTime2");

            builder.Property(x => x.DtCriacao).IsRequired().HasColumnName("DataCriacao").HasColumnType("DateTime2");

            builder.Property(x => x.DtAtualizacao).IsRequired(false).HasColumnName("DataAtualizacao").HasColumnType("DateTime2");

            builder.OwnsOne(x => x.Email, tf => 
            {
                tf.Property(x => x.Address).IsRequired().HasColumnName("Email").HasColumnType($"varchar({Email.EMAIL_TAM_MAXIMO})");            
            });

            builder.OwnsOne(x => x.Cpf, tf =>
            {
                tf.Property(x => x.Numero).IsRequired().HasColumnName("Cpf").HasColumnType($"varchar({Cpf.TAM_MAXIMO})");
            });

            builder.OwnsOne(x => x.Senha, tf =>
            {
                tf.Property(x => x.Valor).IsRequired().HasColumnName("Senha").HasColumnType("varchar(150)");
            });

            builder.HasMany(x => x.Cartoes).WithOne();
            builder.HasMany(x => x.Assinaturas).WithOne();
            builder.HasMany(x => x.Playlists).WithOne(x => x.Usuario);
        }
    }
}
