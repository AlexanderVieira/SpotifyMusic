using AutoMapper;
using AVS.SpotifyMusic.Application.Contas.DTOs;
using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Application.Contas.Interfaces.Services;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using AVS.SpotifyMusic.Domain.Pagamentos.Entidades;
using AVS.SpotifyMusic.Domain.Pagamentos.Enums;
using AVS.SpotifyMusic.Domain.Streaming.Enums;
using AVS.SpotifyMusic.Application.Streamings.DTOs;
using AVS.SpotifyMusic.Domain.Core.Services.WebApi.AspNetUser.Interfaces;


namespace AVS.SpotifyMusic.Application.AppServices
{
	public class UsuarioAppService
	{
		private readonly IUsuarioService _usuarioService;
        private readonly IBandaService _bandaService;
        private readonly IMapper _mapper;
        private readonly IAspNetUser _aspNetUser;

        public UsuarioAppService(
			IUsuarioService usuarioService, 
			IBandaService bandaService,
			IMapper mapper, 
			IAspNetUser aspNetUser)
		{
			_usuarioService = usuarioService;
            _bandaService = bandaService;
            _mapper = mapper;
            _aspNetUser = aspNetUser;
        }

        public async Task<IEnumerable<UsuarioConsultaAnonima>> BuscarTodosPorNomeConsultaProjetada(string filtro)
        {
            var response = await _usuarioService.BuscarPorCriterioConsultaProjetada(x => x.Nome.ToLower().Contains(filtro.ToLower()));            
            return response;
        }

        public async Task<IEnumerable<UsuarioConsultaAnonima>> BuscarTodosConsultaProjetada()
        {
            var response = await _usuarioService.BuscarTodosConsultaProjetada();
            //var response = _mapper.Map<IEnumerable<UsuarioResponse>>(usuarios);
            return response;
        }

        public async Task<IEnumerable<UsuarioResponse>> ObterTodos()
		{
			var usuarios = await _usuarioService.ObterTodos();
			var response = _mapper.Map<IEnumerable<UsuarioResponse>>(usuarios);
			return response;
		}

		public async Task<UsuarioDetalheResponse> ObterPorId(Guid id)
		{
			var usuario = await _usuarioService.ObterPorId(id);			
			var response = _mapper.Map<UsuarioDetalheResponse>(usuario);
			return response;
		}

		public async Task<bool> Ativar(Guid id)
		{
			var response = await _usuarioService.Ativar(id);			
			return response;
		}

		public async Task<bool> Inativar(Guid id)
		{
			var response = await _usuarioService.Inativar(id);
			return response;
		}

		public async Task<IEnumerable<UsuarioResponse>> BuscarTodosPorNome(string filtro)
		{
			var usuarios = await _usuarioService.BuscarTodosPorCriterio(u => u.Nome.ToLower().Contains(filtro.ToLower()));
			var response = _mapper.Map<IEnumerable<UsuarioResponse>>(usuarios);
			return response;
		}

		public async Task<UsuarioDetalheResponse> UsuarioDetalhe(Guid id)
		{			
			var usuario = await _usuarioService.BuscarPorCriterioDetalhado(u => u.Id == id);
			var response = _mapper.Map<UsuarioDetalheResponse>(usuario);
			return response;
		}

		public async Task<bool> Criar(UsuarioRequest request)
		{
			if (await UsuarioExiste(request.Email))
				throw new DomainException("Usuário já existe na base de dados.");

			var tipoPlano = request.Assinatura.Plano.TipoPlano;
			var plano = SelecionarPlano(tipoPlano);			

			var cartao = _mapper.Map<Cartao>(request.Cartao);
			var pagamento = new Pagamento(plano.Valor, 
										  StatusPagamento.Pago, 
										  cartao, 
										  new Transacao(plano.Valor, plano.Nome, StatusTransacao.Pago));
			cartao.Pagamento = pagamento;
            var usuario = _mapper.Map<Usuario>(request);
			usuario.CriarConta(plano, cartao.Pagamento);

			var response = await _usuarioService.Salvar(usuario);
			return response;
		}

		public async Task<bool> Atualizar(UsuarioAtualizaRequest request)
		{
            if (!await UsuarioExiste(request.Id))
                throw new DomainException("Usuário não existe na base de dados.");

			var usuarioParaAtualizar = await _usuarioService.BuscarPorCriterio(u => 
														   u.Email.Address.ToLower() == request.Email.ToLower());

			usuarioParaAtualizar.Atualizar(request.Nome, 
										   request.Email, 
										   request.Cpf, 
										   request.Senha, 
										   request.Ativo, 
										   request.DtNascimento, 
										   request.Foto);

			var response = await _usuarioService.Atualizar(usuarioParaAtualizar);
			return response;
		}

		public async Task<bool> AdicionarMusicaPlaylist(Guid userId, Guid bandaId, Guid musicaId)
		{
			var usuario = await _usuarioService.BuscarPorCriterioDetalhado(u => u.Id == userId);
			
			var banda = await _bandaService.BuscarPorCriterioDetalhado(b => b.Id == bandaId);
			if(banda == null) return false;
			
			var musica = banda.Albuns.SelectMany(a => a.Musicas).FirstOrDefault(m => m.Id == musicaId);
			if (musica == null) return false;
			
			var playlist = usuario.Playlists.Select(p => p).FirstOrDefault(p => p.Titulo.ToLower().Equals("Minha Playlist Nº 1".ToLower()));
			if (playlist == null) return false;
			
			if(usuario.Playlists.Select(x => x).ToList().Contains(playlist))
				usuario.Playlists.Remove(playlist);
			
			if(playlist.Musicas.Any(m => m.Id == musicaId))
				return false;

			playlist.Musicas.Add(musica);
			usuario.AdicionarPlaylist(playlist);

			var response = await _usuarioService.Atualizar(usuario);
			return response;
		}

		public async Task<bool> Remover(Guid id)
		{
			var usuario = await _usuarioService.BuscarPorCriterioDetalhado(x => x.Id == id);
			if (usuario == null) return false;
            
			usuario.LimparCartoes();
            usuario.LimparAssinaturas();
            usuario.LimparPlaylists();
            usuario.LimparNotificacao();            

            var response = await _usuarioService.Remover(id);
			return response;
		}

		public async Task<IEnumerable<PlaylistResponse>> ObterPlaylistsPorId(Guid id)
		{	
			if (!await UsuarioExiste(id))
				throw new DomainException("Usuário não existe na base de dados.");
		
			var usuario = await _usuarioService.BuscarPorCriterioDetalhado(u => u.Id == id);
			var usuarioResponse = _mapper.Map<UsuarioDetalheResponse>(usuario);
			var response = usuarioResponse.Playlists;
			return response;
		}

		private async Task<bool> UsuarioExiste(string filtro)
		{
			var result = await _usuarioService.Existe(u => u.Email.Address.ToLower().Equals(filtro.ToLower()));
			return result;
        }

        private async Task<bool> UsuarioExiste(Guid id)
        {
            var result = await _usuarioService.Existe(u => u.Id == id);
            return result;
        }

        private Plano SelecionarPlano(int tipoPlano)
		{

            return tipoPlano switch
            {
                (int)TipoPlano.Basico => new Plano("Básico", "Plano gratuito", 0.0M, TipoPlano.Basico),
                (int)TipoPlano.Familia => new Plano("Familia", "Baixar Musicas", 9.90M, TipoPlano.Familia),
                (int)TipoPlano.Premium => new Plano("Premium", "Outros Beneficios", 14.90M, TipoPlano.Premium),
                _ => null,
            };
        }

	}
}
