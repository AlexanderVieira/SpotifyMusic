﻿using AVS.SpotifyMusic.Api.Extensions;
using AVS.SpotifyMusic.Api.Services;
using AVS.SpotifyMusic.Api.Services.Interfaces;
using AVS.SpotifyMusic.Application.AppServices;
using AVS.SpotifyMusic.Application.Contas.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AVS.SpotifyMusic.Api.Controllers
{
    [Route("api/contas")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class ContasController : ControllerBase
	{
		private readonly UsuarioAppService _usuarioAppService;
		private readonly AuthService _authService;
		private readonly IUploadService _uploadService;
		private readonly string _destino = "Perfil";
        

		public ContasController(UsuarioAppService usuarioAppService, AuthService authService, IUploadService uploadService)
		{
            _uploadService = uploadService;
			_usuarioAppService = usuarioAppService;
			_authService = authService;			
		}

        [HttpGet]
        [Route("usuarios")]
        public async Task<IActionResult> ObterTodos()
        {
            var response = await _usuarioAppService.ObterTodos();
            if (response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet]
		[Route("usuarios/todos")]
		public async Task<IActionResult> ObterTodosConsultaProjetada()
		{
			var response = await _usuarioAppService.BuscarTodosConsultaProjetada();
			if(response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status200OK, response);
		}

        [HttpGet]
        [Route("usuarios/filtro/{nome}")]
        public async Task<IActionResult> ObterTodosPorNome(string nome)
        {
            var response = await _usuarioAppService.BuscarTodosPorNomeConsultaProjetada(nome);
            if (response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
            return StatusCode(StatusCodes.Status200OK, response);
        }

        [HttpGet]
		[Route("usuarios/{filtro}")]
		public async Task<IActionResult> BuscarTodosPorNome(string filtro)
		{			
			var response = await _usuarioAppService.BuscarTodosPorNome(filtro);
			if (response == null || !response.Any()) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status200OK, response);
		}


		[HttpGet]
		[Route("usuario-detalhe/{id:Guid}")]
		public async Task<IActionResult> UsuarioDetalhe(Guid id)
		{
			var response = await _usuarioAppService.UsuarioDetalhe(id);
			if (response == null) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status200OK, response);
		}

		[HttpPost]
		[Route("usuario-criar")]
		public async Task<IActionResult> Criar(UsuarioRequest request)
		{
            if (!ModelState.IsValid) return BadRequest();
            var response = await _usuarioAppService.Criar(request);			
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();
			return StatusCode(StatusCodes.Status201Created, url);
		}

		[HttpPut]
		[Route("usuario-atualizar")]
		public async Task<IActionResult> Atualizar(UsuarioAtualizaRequest request)
		{
            if (!ModelState.IsValid) return BadRequest();
            var response = await _usuarioAppService.Atualizar(request);
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();
			return StatusCode(StatusCodes.Status200OK,url);
		}

		[HttpDelete]
		[Route("usuario-remover/{id:Guid}")]
		public async Task<IActionResult> Remover(Guid id)
		{
            if (!ModelState.IsValid) return BadRequest();
            var response = await _usuarioAppService.Remover(id);
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();
			return StatusCode(StatusCodes.Status204NoContent, url);
		}

		[HttpPut]
		[Route("usuario-ativar/{id:Guid}")]
		public async Task<IActionResult> Ativar(Guid id)
		{
            if (!ModelState.IsValid) return BadRequest();
            var response = await _usuarioAppService.Ativar(id);
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();			
			return StatusCode(StatusCodes.Status200OK, url);
		}

		[HttpPut]
		[Route("usuario-inativar/{id:Guid}")]
		public async Task<IActionResult> Inativar(Guid id)
		{
            if (!ModelState.IsValid) return BadRequest();
            var response = await _usuarioAppService.Inativar(id);
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();
			return StatusCode(StatusCodes.Status200OK, url);
		}

		[HttpGet]
		[Route("usuario/playlists/{id:Guid}")]
		public async Task<IActionResult> ObterPlaylistDoUsuario(Guid id)
		{
			var response = await _usuarioAppService.ObterPlaylistsPorId(id);
			if (response == null) { return StatusCode(StatusCodes.Status404NotFound); }
			return StatusCode(StatusCodes.Status200OK, response);
		}

		[HttpPut]
		[Route("usuario-adicionar-musica/{bandaId:Guid}/{musicaId:Guid}")]
		public async Task<IActionResult> AdicionarMusicaPlaylistUsuario(Guid bandaId, Guid musicaId)
		{
            if (!ModelState.IsValid) return BadRequest();
			var userClaim =_authService._aspNetUser.GetHttpContext().User;
			var userId = Guid.Parse( _authService.UserManager.GetUserId(userClaim));
            var response = await _usuarioAppService.AdicionarMusicaPlaylist(userId, bandaId, musicaId);
			if (response == false) return BadRequest();
			var url = HttpContext.Request.GetUrl();
			return StatusCode(StatusCodes.Status200OK,url);
		}

		[HttpPost("upload-image/{userId:Guid}")]
        public async Task<IActionResult> UploadImage(Guid userId)
        {
            try
            {
                var user = await _usuarioAppService.ObterPorId(userId);
                if (user == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    _uploadService.DeleteImage(file.FileName, _destino);
                    user.Foto = await _uploadService.SaveImage(file, _destino);
                }
				var userRequest = new UsuarioAtualizaRequest
				{
					Id = user.Id,
					Nome = user.Nome,
					Email = user.Email,
					Cpf = user.Cpf,
					Foto = user.Foto,
					DtNascimento = user.DtNascimento,
					Ativo = user.Ativo,
					Senha = user.Senha
				};
                var response = await _usuarioAppService.Atualizar(userRequest);
				if(response == false) return BadRequest($"Erro ao tentar realizar upload de Foto do Usuário.");
                var url = HttpContext.Request.GetUrl();
				return StatusCode(StatusCodes.Status200OK, url);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar realizar upload de Foto do Usuário. Erro: {ex.Message}");
            }
        }

	}
}
