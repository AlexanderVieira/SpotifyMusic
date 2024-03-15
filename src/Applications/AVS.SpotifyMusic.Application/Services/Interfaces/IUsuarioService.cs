using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using System.Linq.Expressions;

namespace AVS.SpotifyMusic.Application.Contas.Interfaces.Services
{
    public interface IUsuarioService : IService<Usuario>
    {
        Task<bool> Ativar(Guid usuarioId);
		Task<bool> Inativar(Guid usuarioId);
        Task<bool> Existe(Expression<Func<Usuario, bool>> expression);
        Task<Usuario> BuscarPorCriterioDetalhado(Expression<Func<Usuario, bool>> expression);
        Task<IEnumerable<UsuarioConsultaAnonima>> BuscarTodosConsultaProjetada();
        Task<IEnumerable<UsuarioConsultaAnonima>> BuscarPorCriterioConsultaProjetada(Expression<Func<Usuario, bool>> expression);

    }
}
