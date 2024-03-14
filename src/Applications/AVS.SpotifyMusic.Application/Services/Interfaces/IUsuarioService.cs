using AVS.SpotifyMusic.Domain.Contas.Entidades;
using System.Linq.Expressions;

namespace AVS.SpotifyMusic.Application.Contas.Interfaces.Services
{
    public interface IUsuarioService : IService<Usuario>
    {
        Task<bool> Ativar(Guid usuarioId);
		Task<bool> Inativar(Guid usuarioId);
        Task<bool> Existe(Expression<Func<Usuario, bool>> expression);

    }
}
