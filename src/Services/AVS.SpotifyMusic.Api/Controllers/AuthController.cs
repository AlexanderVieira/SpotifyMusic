using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVS.SpotifyMusic.Api.Models;
using AVS.SpotifyMusic.Api.Services;
using AVS.SpotifyMusic.Domain.Core.Services.WebApi.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AVS.SpotifyMusic.Api.Controllers
{   
    [Route("api/auth")]
    public class AuthController : MainController
    {
        private readonly AuthService _authService;        

        public AuthController(AuthService authService)
        {
            _authService = authService;            
        }       

        [HttpPost("signup")]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {

            if (!ModelState.IsValid) return RespostaPersonalizada(ModelState);

            var user = new IdentityUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = true
            };

            var result = await _authService.UserManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {               
                return RespostaPersonalizada(await _authService.GenerateJwt(userRegister.Email));
            }

            foreach (var error in result.Errors)
            {
                AdicionarErroProcessamento(error.Description);
            }

            return RespostaPersonalizada();
        }

        [HttpPost("signin")]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {

            if (!ModelState.IsValid) return RespostaPersonalizada(ModelState);

            var result = await _authService.SignInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

            if (result.Succeeded)
            {
                return RespostaPersonalizada(await _authService.GenerateJwt(userLogin.Email));
            }

            if (result.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário temporariamente bloqueado por tentativas inválidas");
                return RespostaPersonalizada();
            }

            AdicionarErroProcessamento("Usuário ou Senha incorreto");
            return RespostaPersonalizada();
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {

            if (!ModelState.IsValid) return RespostaPersonalizada(ModelState);

            var loggedOut = await _authService.Logout();

            if (loggedOut == true)
            {
                AdicionaMensagemSucesso("Usuário saiu do sistema");
                return RespostaPersonalizada(StatusCodes.Status200OK);
            }

            AdicionarErroProcessamento("Ocorreu um erro ao tentar sair do sistema");
            return RespostaPersonalizada();
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                AdicionarErroProcessamento("Refresh Token inválido");
                return RespostaPersonalizada();
            }            

            if (!await _authService.RefreshTokenValido(refreshToken))
            {
                AdicionarErroProcessamento("Refresh Token expirado");
                return RespostaPersonalizada();
            }

            var token = await _authService.GetRefreshToken(Guid.Parse(refreshToken));

            return RespostaPersonalizada(await _authService.GenerateJwt(token.Username));
        }
        
    }
}