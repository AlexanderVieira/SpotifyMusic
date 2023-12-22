using AVS.SpotifyMusic.Domain.Contas.Entidades;

namespace AVS.SpotifyMusic.Domain.Contas.Interfaces.Services
{
    public interface IUsuarioService : IService<Usuario>
    {
        void Ativar(Guid usuarioId);
        void Inativar(Guid usuarioId);

    }
}
