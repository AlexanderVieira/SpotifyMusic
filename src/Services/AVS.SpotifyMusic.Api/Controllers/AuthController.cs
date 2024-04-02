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

        [HttpPost("singin")]
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

        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                AdicionarErroProcessamento("Refresh Token inválido");
                return RespostaPersonalizada();
            }

            var token = await _authService.GetRefreshToken(Guid.Parse(refreshToken));

            if (token is null)
            {
                AdicionarErroProcessamento("Refresh Token expirado");
                return RespostaPersonalizada();
            }

            return RespostaPersonalizada(await _authService.GenerateJwt(token.Username));
        }
        
    }
}