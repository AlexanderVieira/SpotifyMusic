using AVS.SpotifyMusic.Api.Extensions;
using AVS.SpotifyMusic.Application.AppServices;
using AVS.SpotifyMusic.Application.Contas.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AVS.SpotifyMusic.Api.Controllers
{
	[Route("api/contas")]
	[ApiController]
	public class ContasController : ControllerBase
	{
		private readonly UsuarioAppService _usuarioAppService;

		public ContasController(UsuarioAppService usuarioAppService)
		{
			_usuarioAppService = usuarioAppService;
		}

		[HttpGet]
		[Route("usuarios")]
		public async Task<IActionResult> ObterTodos()
		{
			var response = await _usuarioAppService.ObterTodos();
			if(response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status201Created, response);
		}

		[HttpGet]
		[Route("usuarios/{filtro}")]
		public async Task<IActionResult> BuscarTodosPorNome(string filtro)
		{			
			var response = await _usuarioAppService.BuscarTodosPorNome(filtro);
			if (response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status201Created, response);
		}


		[HttpGet]
		[Route("usuario-detalhe/{id:Guid}")]
		public async Task<IActionResult> UsuarioDetalhe(Guid id)
		{
			var response = await _usuarioAppService.UsuarioDetalhe(id);
			if (response == null) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status201Created, response);
		}

		[HttpPost]
		[Route("usuario-criar")]
		public async Task<IActionResult> Criar(UsuarioDto request)
		{
			var response = await _usuarioAppService.Criar(request);			
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();
			return StatusCode(StatusCodes.Status201Created, url);
		}

		[HttpPut]
		[Route("usuario-atualizar")]
		public async Task<IActionResult> Atualizar(UsuarioDto request)
		{
			var response = await _usuarioAppService.Atualizar(request);
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();
			return StatusCode(StatusCodes.Status200OK,url);
		}

		[HttpDelete]
		[Route("usuario-remover/{id:Guid}")]
		public async Task<IActionResult> Remover(Guid id)
		{
			var response = await _usuarioAppService.Remover(id);
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();
			return StatusCode(StatusCodes.Status204NoContent, url);
		}

		[HttpPut]
		[Route("usuario-ativar/{id:Guid}")]
		public async Task<IActionResult> Ativar(Guid id)
		{
			var response = await _usuarioAppService.Ativar(id);
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();			
			return StatusCode(StatusCodes.Status200OK, url);
		}

		[HttpPut]
		[Route("usuario-inativar/{id:Guid}")]
		public async Task<IActionResult> Inativar(Guid id)
		{
			var response = await _usuarioAppService.Inativar(id);
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();
			return StatusCode(StatusCodes.Status200OK, url);
		}

	}
}
