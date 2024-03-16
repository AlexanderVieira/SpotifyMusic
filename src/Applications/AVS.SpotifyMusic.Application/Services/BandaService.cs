using AVS.SpotifyMusic.Application.Contas.Interfaces.Services;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using AVS.SpotifyMusic.Domain.Streaming.Interfaces.Repositories;
using System.Linq.Expressions;

namespace AVS.SpotifyMusic.Application.Contas.Services
{
    public class BandaService : BaseService<Banda>, IBandaService
    {
        
        private readonly IBandaRepository _bandaRepository;

        public BandaService(IBandaRepository bandaRepository) : base(bandaRepository)
        {
            _bandaRepository = bandaRepository;
        }

        public async Task<IEnumerable<BandaConsultaAnonima>> BuscarPorCriterioConsultaProjetada(Expression<Func<Banda, bool>> expression)
        {
            var result = await _bandaRepository.BuscarPorCriterioConsultaProjetada(expression);
            return result;
        }

        public async Task<Banda> BuscarPorCriterioDetalhado(Expression<Func<Banda, bool>> expression)
        {
            var result = await _bandaRepository.BuscarPorCriterioDetalhado(expression);
            return result;
        }

        public async Task<IEnumerable<BandaConsultaAnonima>> BuscarTodosConsultaProjetada()
        {
            var result = await _bandaRepository.BuscarTodosConsultaProjetada();           
            return result;
        }

        public async Task<bool> Existe(Expression<Func<Banda, bool>> expression)
        {
            var result = await _bandaRepository.Existe(expression);
            return result;
        }       

    }
}
