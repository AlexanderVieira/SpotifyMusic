using AutoMapper;
using AVS.SpotifyMusic.Application.Contas.DTOs;
using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Application.Contas.Interfaces.Services;


namespace AVS.SpotifyMusic.Application.AppServices
{
	public class UsuarioAppService // : BaseAppService<UsuarioDto>
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

		public async Task<bool> Criar(UsuarioDto usuarioDto)
		{			
			var usuario = _mapper.Map<Usuario>(usuarioDto);
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

	}
}
