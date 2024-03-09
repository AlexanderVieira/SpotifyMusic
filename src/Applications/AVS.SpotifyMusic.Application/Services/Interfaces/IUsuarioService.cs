using AVS.SpotifyMusic.Domain.Contas.Entidades;

namespace AVS.SpotifyMusic.Application.Contas.Interfaces.Services
{
    public interface IUsuarioService : IService<Usuario>
    {
        Task<bool> Ativar(Guid usuarioId);
		Task<bool> Inativar(Guid usuarioId);

    }
}
