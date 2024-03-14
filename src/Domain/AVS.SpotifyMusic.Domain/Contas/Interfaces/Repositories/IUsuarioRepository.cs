using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Core.Data;
using System.Linq.Expressions;

namespace AVS.SpotifyMusic.Domain.Contas.Interfaces.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<bool> Existe(Expression<Func<Usuario, bool>> expression);

    }
}
