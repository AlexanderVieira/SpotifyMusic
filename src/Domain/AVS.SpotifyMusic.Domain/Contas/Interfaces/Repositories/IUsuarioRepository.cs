using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Core.Data;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using System.Linq.Expressions;

namespace AVS.SpotifyMusic.Domain.Contas.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<bool> Existe(Expression<Func<Usuario, bool>> expression);
        Task<Usuario> BuscarPorCriterioDetalhado(Expression<Func<Usuario, bool>> expression);
        Task<IEnumerable<UsuarioConsultaAnonima>> BuscarPorCriterioConsultaProjetada(Expression<Func<Usuario, bool>> expression);

    }
}
