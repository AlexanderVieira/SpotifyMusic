using AutoMapper;
using AVS.SpotifyMusic.Application.Contas.DTOs;
using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Application.Contas.Interfaces.Services;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using AVS.SpotifyMusic.Domain.Pagamentos.Entidades;
using AVS.SpotifyMusic.Domain.Pagamentos.Enums;
using AVS.SpotifyMusic.Domain.Streaming.Enums;


namespace AVS.SpotifyMusic.Application.AppServices
{
	public class UsuarioAppService
	{
		private readonly IUsuarioService _usuarioService;
		private readonly IMapper _mapper;

		public UsuarioAppService(IUsuarioService usuarioService, IMapper mapper)
		{
			_usuarioService = usuarioService;
			_mapper = mapper;
		}

		public async Task<IEnumerable<UsuarioDto>> ObterTodos()
		{
			var usuario = await _usuarioService.ObterTodos();
			var response = _mapper.Map<IEnumerable<UsuarioDto>>(usuario);
			return response;
		}

		public async Task<UsuarioDto> ObterPorId(Guid id)
		{
			var usuario = await _usuarioService.ObterPorId(id);			
			var response = _mapper.Map<UsuarioDto>(usuario);
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

		public async Task<IEnumerable<UsuarioDto>> BuscarTodosPorNome(string filtro)
		{
			var usuarios = await _usuarioService.BuscarTodosPorCriterio(u => u.Nome.ToLower().Contains(filtro.ToLower()));
			var response = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
			return response;
		}

		public async Task<UsuarioDto> UsuarioDetalhe(Guid id)
		{			
			var usuario = await _usuarioService.BuscarPorCriterio(u => u.Id == id);
			var response = _mapper.Map<UsuarioDto>(usuario);
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
										  StatusPagamento.Processando, 
										  cartao, 
										  new Transacao(plano.Valor, "Merchant Teste", StatusTransacao.Pendente));
			cartao.Pagamento = pagamento;
            var usuario = _mapper.Map<Usuario>(request);
			usuario.CriarConta(plano, cartao.Pagamento);

			var response = await _usuarioService.Salvar(usuario);
			return response;
		}

		public async Task<bool> Atualizar(UsuarioDto usuarioDto)
		{
			var usuario = _mapper.Map<Usuario>(usuarioDto);
			var response = await _usuarioService.Atualizar(usuario);
			return response;
		}

		public async Task<bool> Remover(Guid id)
		{
			var response = await _usuarioService.Remover(id);
			return response;
		}

		private async Task<bool> UsuarioExiste(string filtro)
		{
			var result = await _usuarioService.Existe(u => u.Email.Address.ToLower().Equals(filtro.ToLower()));
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
