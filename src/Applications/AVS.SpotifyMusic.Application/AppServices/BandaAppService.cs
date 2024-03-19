using AutoMapper;
using AVS.SpotifyMusic.Application.Contas.Interfaces.Services;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Application.Streamings.DTOs;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;


namespace AVS.SpotifyMusic.Application.AppServices
{
    public class BandaAppService
	{
		private readonly IBandaService _bandaService;
		private readonly IMapper _mapper;

		public BandaAppService(IBandaService bandaService, IMapper mapper)
		{
            _bandaService = bandaService;
			_mapper = mapper;
		}

        public async Task<IEnumerable<BandaConsultaAnonima>> BuscarTodosPorNomeConsultaProjetada(string filtro)
        {
            var response = await _bandaService.BuscarPorCriterioConsultaProjetada(x => x.Nome.ToLower().Contains(filtro.ToLower()));            
            return response;
        }

        public async Task<IEnumerable<BandaConsultaAnonima>> BuscarTodosConsultaProjetada()
        {
            var response = await _bandaService.BuscarTodosConsultaProjetada();            
            return response;
        }

        public async Task<IEnumerable<BandaResponse>> ObterTodos()
		{
			var bandas = await _bandaService.ObterTodos();
			var response = _mapper.Map<IEnumerable<BandaResponse>>(bandas);
			return response;
		}

		public async Task<BandaResponse> ObterPorId(Guid id)
		{
			var banda = await _bandaService.ObterPorId(id);			
			var response = _mapper.Map<BandaResponse>(banda);
			return response;
		}		

		public async Task<IEnumerable<BandaResponse>> BuscarTodosPorNome(string filtro)
		{
			var bandas = await _bandaService.BuscarTodosPorCriterio(u => u.Nome.ToLower().Contains(filtro.ToLower()));
			var response = _mapper.Map<IEnumerable<BandaResponse>>(bandas);
			return response;
		}

		public async Task<BandaDetalheResponse> ObterDetalhe(Guid id)
		{			
			var banda = await _bandaService.BuscarPorCriterioDetalhado(u => u.Id == id);
			var response = _mapper.Map<BandaDetalheResponse>(banda);
			response.Albuns.ToList().ForEach(x => x.BandaId = response.Id);
			return response;
		}

		public async Task<bool> Criar(BandaRequest request)
		{
            var banda = _mapper.Map<Banda>(request);           
			var response = await _bandaService.Salvar(banda);
			return response;
		}

		public async Task<bool> Atualizar(BandaRequest request)
		{
            if (!await BandaExiste(request.Id.Value))
                throw new DomainException("Banda não existe na base de dados.");

			var bandaParaAtualizar = await _bandaService.ObterPorId(request.Id.Value);

            bandaParaAtualizar.Atualizar(request.Nome, 
										 request.Descricao,										  
										 request.Foto);

			var response = await _bandaService.Atualizar(bandaParaAtualizar);
			return response;
		}

		public async Task<bool> Remover(Guid id)
		{
			var response = await _bandaService.Remover(id);
			return response;
		}

        public async Task<bool> CriarAlbum(AlbumRequest request)
        {
			if (!await BandaExiste(request.BandaId))
				throw new DomainException("Banda não existe na base de dados.");

			var banda = await _bandaService.BuscarPorCriterioDetalhado(x => x.Id == request.BandaId);
			var album = _mapper.Map<Album>(request);
			banda.AdicionarAlbum(album);
			//banda.CriarAlbum(request.Titulo, request.Descricao, request.Foto, album.Musicas);
            var response = await _bandaService.CriarAlbum(banda);
            return response;
        }

		public async Task<AlbumResponse> ObterAlbumDetalhe(Guid bandaId, Guid albumId)
		{
			if (!await BandaExiste(bandaId))
				throw new DomainException("Banda não existe na base de dados.");

			var banda = await _bandaService.BuscarPorCriterioDetalhado(x => x.Id == bandaId);
			var album = banda.Albuns.Select(x => x).FirstOrDefault(x => x.Id == albumId);
			if(album == null) 
				throw new DomainException("Album não existe na base de dados.");
			var response = _mapper.Map<AlbumResponse>(album);
			response.BandaId = banda.Id;
			return response;
			
		}

        private async Task<bool> BandaExiste(Guid id)
        {
            var result = await _bandaService.Existe(u => u.Id == id);
            return result;
        }       

	}
}
