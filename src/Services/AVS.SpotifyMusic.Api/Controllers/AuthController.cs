using AVS.SpotifyMusic.Api.Models;
using AVS.SpotifyMusic.Api.Services;
using AVS.SpotifyMusic.Application.AppServices;
using AVS.SpotifyMusic.Application.Contas.DTOs;
using AVS.SpotifyMusic.Application.Pagamentos.DTOs;
using AVS.SpotifyMusic.Application.Streamings.DTOs;
using AVS.SpotifyMusic.Domain.Core.Services.WebApi.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AVS.SpotifyMusic.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : MainController
    {
        private readonly AuthService _authService;        
        private readonly UsuarioAppService _usuarioAppService;

        public AuthController(AuthService authService, UsuarioAppService usuarioAppService)
        {
            _usuarioAppService = usuarioAppService;
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
                var userRequest = new UsuarioRequest{
                    Id = Guid.Parse(user.Id),
                    Nome = $"{userRegister.PrimeiroNome} {userRegister.UltimoNome}",
                    Email = userRegister.Email,
                    Cpf = userRegister.Cpf,
                    Senha = userRegister.Password,
                    DtNascimento =  DateTime.Parse(userRegister.DtNascimento),
                    Ativo = userRegister.Ativo,
                    Cartao = new CartaoRequest{
                        Nome = userRegister.Cartao.Nome,
                        Numero = userRegister.Cartao.Numero,
                        Expiracao = userRegister.Cartao.Expiracao,
                        Cvv = userRegister.Cartao.Cvv,
                        Limite = userRegister.Cartao.Limite,
                    },
                    Assinatura = new AssinaturaRequest{
                        Ativo = userRegister.Assinatura.Ativo,
                        Plano = new PlanoRequest{
                            Nome = userRegister.Assinatura.Plano.Nome,
                            Descricao = userRegister.Assinatura.Plano.Descricao,
                            Valor = userRegister.Assinatura.Plano.Valor,
                            TipoPlano = userRegister.Assinatura.Plano.TipoPlano
                        }
                    }
                     
                };   

                var response = await _usuarioAppService.Criar(userRequest);
                if(response == false) RespostaPersonalizada(StatusCodes.Status400BadRequest);

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