using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Core.Data;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using System.Linq.Expressions;

namespace AVS.SpotifyMusic.Domain.Streaming.Interfaces.Repositories
{
    public interface IBandaRepository : IRepository<Banda>
    {
        Task<bool> Existe(Expression<Func<Banda, bool>> expression);
        Task<Banda> BuscarPorCriterioDetalhado(Expression<Func<Banda, bool>> expression);
        Task<IEnumerable<BandaConsultaAnonima>> BuscarTodosConsultaProjetada();
        Task<IEnumerable<BandaConsultaAnonima>> BuscarPorCriterioConsultaProjetada(Expression<Func<Banda, bool>> expression);

    }
}
