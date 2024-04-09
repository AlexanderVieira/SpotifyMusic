using AVS.SpotifyMusic.Api.Extensions;
using AVS.SpotifyMusic.Api.Services.Interfaces;
using AVS.SpotifyMusic.Application.AppServices;
using AVS.SpotifyMusic.Application.Streamings.DTOs;
using AVS.SpotifyMusic.Domain.Core.Services.WebApi.Controllers;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AVS.SpotifyMusic.Api.Controllers
{
    [Route("api/streaming")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BandasController : MainController
    {
		private readonly BandaAppService _bandaAppService;
        private readonly IUploadService _uploadService;
		private readonly string _destino = "Banda";
        

		public BandasController(BandaAppService bandaAppService, IUploadService uploadService)
		{
            _uploadService = uploadService;
            _bandaAppService = bandaAppService;         
		}
		
        [HttpGet]
        [Route("bandas")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterTodasBandas()
        {
            var response = await _bandaAppService.ObterTodos();
            if (response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
            return StatusCode(StatusCodes.Status200OK, response.ToArray());
        }

        [HttpGet]
		[Route("bandas/todos")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterTodasPorConsultaProjetada()
		{
			var response = await _bandaAppService.BuscarTodosConsultaProjetada();
			if(response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status200OK, response.ToArray());
		}

        [HttpGet]
        [Route("bandas/filtro/{nome}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterTodasPorNomeConsultaProjetada(string nome)
        {
            var response = await _bandaAppService.BuscarTodosPorNomeConsultaProjetada(nome);
            if (response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet]
		[Route("bandas/{filtro}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterTodasPorNome(string filtro)
		{			
			var response = await _bandaAppService.BuscarTodosPorNome(filtro);
			if (response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status200OK, response);
		}

        [HttpGet]
        [Route("banda/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterPorId(string id)
        {			
            var response = await _bandaAppService.ObterPorId(Guid.Parse(id));
            if (response == null) { return StatusCode(StatusCodes.Status404NotFound); }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet]
		[Route("banda-detalhe/{id:Guid}")]
		[AllowAnonymous]
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

        [HttpPut]
        [Route("banda/atualizar-album")]
        public async Task<IActionResult> AtualizarAlbum(AlbumRequest request)
        {
			if(!ModelState.IsValid) return BadRequest();
            var response = await _bandaAppService.AtualizarAlbum(request);
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

        //[ClaimsAuthorize("Bandas", "Exclusao")]        
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

        [HttpPost("upload-image/{bandaId:Guid}")]
        public async Task<IActionResult> UploadImage(Guid bandaId)
        {
            try
            {
                 var banda = await _bandaAppService.ObterPorId(bandaId);
                 if (banda == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    _uploadService.DeleteImage(file.FileName, _destino);
                    banda.Foto = await _uploadService.SaveImage(file, _destino);
                }
				var bandaRequest = new BandaRequest
				{
                    Id = banda.Id,
					Nome = banda.Nome,
                    Descricao = banda.Descricao,
                    Foto = banda.Foto                    
				};
                var response = await _bandaAppService.Atualizar(bandaRequest);
                if(response == false) return BadRequest($"Erro ao tentar realizar upload de Foto do Usuário.");
                var url = HttpContext.Request.GetUrl();
				return StatusCode(StatusCodes.Status200OK, url);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar realizar upload da foto da banda. Erro: {ex.Message}");
            }
        }
          
        [HttpPost("upload-image/album/{albumId:Guid}/{bandaId:Guid}")]
        public async Task<IActionResult> UploadImage(Guid albumId, Guid bandaId)
        {
            try
            {
                 var albumResponse = await _bandaAppService.ObterAlbumDetalhe(bandaId, albumId);
                 if (albumResponse == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    _uploadService.DeleteImage(file.FileName, _destino);
                    albumResponse.Foto = await _uploadService.SaveImage(file, _destino);
                }
				var albumResquest = new AlbumRequest
				{        
                    Id = albumResponse.Id,            
					Titulo = albumResponse.Titulo,
                    Descricao = albumResponse.Descricao,
                    Foto = albumResponse.Foto,
                    BandaId = albumResponse.BandaId                    
				};
                var response = await _bandaAppService.AtualizarAlbum(albumResquest);
                if(response == false) return BadRequest($"Erro ao tentar realizar upload de Foto do Usuário.");
                var url = HttpContext.Request.GetUrl();
				return StatusCode(StatusCodes.Status200OK, url);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar realizar upload da foto da banda. Erro: {ex.Message}");
            }
        }	

	}
}
