using AVS.SpotifyMusic.Api.Extensions;
using AVS.SpotifyMusic.Application.AppServices;
using AVS.SpotifyMusic.Application.Contas.DTOs;
using AVS.SpotifyMusic.Application.Streamings.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AVS.SpotifyMusic.Api.Controllers
{
	[Route("api/streaming")]
	[ApiController]
	public class BandasController : ControllerBase
	{
		private readonly BandaAppService _bandaAppService;

		public BandasController(BandaAppService bandaAppService)
		{
            _bandaAppService = bandaAppService;
		}

        [HttpGet]
        [Route("bandas")]
        public async Task<IActionResult> ObterTodos()
        {
            var response = await _bandaAppService.ObterTodos();
            if (response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet]
		[Route("bandas/todos")]
		public async Task<IActionResult> ObterTodosConsultaProjetada()
		{
			var response = await _bandaAppService.BuscarTodosConsultaProjetada();
			if(response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status200OK, response);
		}

        [HttpGet]
        [Route("bandas/filtro/{nome}")]
        public async Task<IActionResult> ObterTodosPorNome(string nome)
        {
            var response = await _bandaAppService.BuscarTodosPorNomeConsultaProjetada(nome);
            if (response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet]
		[Route("bandas/{filtro}")]
		public async Task<IActionResult> BuscarTodosPorNome(string filtro)
		{			
			var response = await _bandaAppService.BuscarTodosPorNome(filtro);
			if (response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status200OK, response);
		}


		[HttpGet]
		[Route("bandas-detalhe/{id:Guid}")]
		public async Task<IActionResult> UsuarioDetalhe(Guid id)
		{
			var response = await _bandaAppService.UsuarioDetalhe(id);
			if (response == null) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status200OK, response);
		}

		[HttpPost]
		[Route("banda-criar")]
		public async Task<IActionResult> Criar(BandaRequest request)
		{
			var response = await _bandaAppService.Criar(request);			
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();
			return StatusCode(StatusCodes.Status201Created, url);
		}

		[HttpPut]
		[Route("banda-atualizar")]
		public async Task<IActionResult> Atualizar(BandaRequest request)
		{			
			var response = await _bandaAppService.Atualizar(request);
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();
			return StatusCode(StatusCodes.Status200OK,url);
		}

		[HttpDelete]
		[Route("banda-remover/{id:Guid}")]
		public async Task<IActionResult> Remover(Guid id)
		{
			var response = await _bandaAppService.Remover(id);
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();
			return StatusCode(StatusCodes.Status204NoContent, url);
		}		

	}
}
