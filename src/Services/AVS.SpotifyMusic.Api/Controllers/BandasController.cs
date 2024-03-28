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
        public async Task<IActionResult> ObterTodasBandas()
        {
            var response = await _bandaAppService.ObterTodos();
            if (response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
            return StatusCode(StatusCodes.Status200OK, response.ToArray());
        }

        [HttpGet]
		[Route("bandas/todos")]
		public async Task<IActionResult> ObterTodasPorConsultaProjetada()
		{
			var response = await _bandaAppService.BuscarTodosConsultaProjetada();
			if(response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status200OK, response.ToArray());
		}

        [HttpGet]
        [Route("bandas/filtro/{nome}")]
        public async Task<IActionResult> ObterTodasPorNomeConsultaProjetada(string nome)
        {
            var response = await _bandaAppService.BuscarTodosPorNomeConsultaProjetada(nome);
            if (response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet]
		[Route("bandas/{filtro}")]
		public async Task<IActionResult> ObterTodasPorNome(string filtro)
		{			
			var response = await _bandaAppService.BuscarTodosPorNome(filtro);
			if (response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status200OK, response);
		}

        [HttpGet]
        [Route("banda/{id}")]
        public async Task<IActionResult> ObterPorId(string id)
        {			
            var response = await _bandaAppService.ObterPorId(Guid.Parse(id));
            if (response == null) { return StatusCode(StatusCodes.Status404NotFound); }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet]
		[Route("banda-detalhe/{id:Guid}")]
		public async Task<IActionResult> Detalhe(Guid id)
		{
			var response = await _bandaAppService.ObterDetalhe(id);
			if (response == null) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status200OK, response);
		}

		[HttpPost]
		[Route("banda-criar")]
		public async Task<IActionResult> Criar(BandaRequest request)
		{
            if (!ModelState.IsValid) return BadRequest();
            var response = await _bandaAppService.Criar(request);			
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();
			return StatusCode(StatusCodes.Status201Created, url);
		}

		[HttpPut]
		[Route("banda-atualizar")]
		public async Task<IActionResult> Atualizar(BandaRequest request)
		{
            if (!ModelState.IsValid) return BadRequest();
            var response = await _bandaAppService.Atualizar(request);
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();
			return StatusCode(StatusCodes.Status200OK,url);
		}

        [HttpPut]
        [Route("banda/criar-album")]
        public async Task<IActionResult> CriarAlbum(AlbumRequest request)
        {
			if(!ModelState.IsValid) return BadRequest();
            var response = await _bandaAppService.CriarAlbum(request);
            if (response == false) return BadRequest();
            var url = HttpContext.Request.GetUrl();
            return StatusCode(StatusCodes.Status200OK, url);
        }

		[HttpGet]
		[Route("banda/{bandaId:Guid}/album-detalhe/{albumId:Guid}")]
		public async Task<IActionResult> AlbumDetalhe(Guid bandaId, Guid albumId)
		{
			var response = await _bandaAppService.ObterAlbumDetalhe(bandaId, albumId);
			if (response == null) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status200OK, response);
		}

        [HttpDelete]
		[Route("banda-remover/{id:Guid}")]
		public async Task<IActionResult> Remover(Guid id)
		{
            if (!ModelState.IsValid) return BadRequest();
            var response = await _bandaAppService.Remover(id);
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();
			return StatusCode(StatusCodes.Status204NoContent, url);
		}		

	}
}
