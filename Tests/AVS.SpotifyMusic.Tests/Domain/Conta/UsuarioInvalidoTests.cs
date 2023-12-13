using AVS.SpotifyMusic.Tests.Builders;
using AVS.SpotifyMusic.Tests.Fixtures;
using FluentAssertions;
using FluentValidation;

namespace AVS.SpotifyMusic.Tests.Domain.Conta
{
    [Collection(nameof(UsuarioCollection))]
    public class UsuarioInvalidoTests 
    {
        private readonly UsuarioFixtureTests _fixture;

        public UsuarioInvalidoTests(UsuarioFixtureTests fixture)
        {
            _fixture = fixture;
        }

        [Trait("Categoria", "Usuario Bogus Testes")]
        [Fact(DisplayName = "Novo Usuario Invalido")]        
        public void Usuario_NovoUsuario_DeveEstarInvalido()
        {
            //Arrange
            var usuario = _fixture.CriarUsuarioInvalido();

            //Act
            var result = usuario.EhValido();

            //Assert            
            result.Should().BeFalse();

        }

        [Trait("Categoria", "Usuario Bogus Testes")]
        [Theory(DisplayName = "Novo Usuario Nome nulo/vazio retorna mensagem")]
        [InlineData("")]
        [InlineData(null)]
        public void Usuario_ValidarNomeNuloVazio_DeveRetornarMensagem(string nomeInvalido)
        {
            //Arrange            
            var usuario = UsuarioBuilder.Novo().ComNome(nomeInvalido).Buid();

            //Act
            var result = usuario.EhValido();

            //Assert            
            Assert.False(result);
            Assert.Contains(usuario?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("Nome é obrigatório."));

        }

        [Trait("Categoria", "Usuario Bogus Testes")]
        [Theory(DisplayName = "Novo Usuario Email nulo/vazio retorna mensagem")]
        [InlineData("")]
        [InlineData(null)]
        public void Usuario_ValidarEmailNuloVazio_DeveRetornarMensagem(string emailInvalido)
        {
            //Arrange            
            var usuario = UsuarioBuilder.Novo().ComEmail(emailInvalido).Buid();

            //Act
            var result = usuario.EhValido();

            //Assert            
            Assert.False(result);
            Assert.Contains(usuario?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("E-mail é obrigatório."));

        }

        [Fact(DisplayName = "Novo Usuario: Email inválido retorna mensagem")]
        [Trait("Categoria", "Usuario Bogus Testes")]
        public void Usuario_ValidarEmailInvalido_DeveRetornarMensagem()
        {
            //Arrange            
            var usuario = UsuarioBuilder.Novo().ComEmail("teste.gmail.com").Buid();

            //Act
            var result = usuario.EhValido();

            //Assert            
            Assert.False(result);
            Assert.Contains(usuario?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("E-mail inválido."));

        }

        [Trait("Categoria", "Usuario Bogus Testes")]
        [Theory(DisplayName = "Novo Usuario CPF nulo/vazio retorna mensagem")]
        [InlineData("")]
        [InlineData(null)]
        public void Usuario_ValidarCpfNuloVazio_DeveRetornarMensagem(string cpfInvalido)
        {
            //Arrange            
            var usuario = UsuarioBuilder.Novo().ComCpf(cpfInvalido).Buid();

            //Act
            var result = usuario.EhValido();

            //Assert            
            Assert.False(result);
            Assert.Contains(usuario?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("Documento é obrigatório."));

        }

        [Trait("Categoria", "Usuario Bogus Testes")]
        [Theory(DisplayName = "Novo Usuario CPF inválido retorna mensagem")]
        [InlineData("19100000001")]
        [InlineData("1910000000")]
        [InlineData("191000000000")]
        [InlineData("11111111111")]
        [InlineData("1234567890")]
        public void Usuario_ValidarCpfInvalido_DeveRetornarMensagem(string cpfInvalido)
        {
            //Arrange            
            var usuario = UsuarioBuilder.Novo().ComCpf(cpfInvalido).Buid();

            //Act
            var result = usuario.EhValido();

            //Assert            
            Assert.False(result);
            Assert.Contains(usuario?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("Documento inválido."));

        }

        [Trait("Categoria", "Usuario Bogus Testes")]
        [Theory(DisplayName = "Novo Usuario Foto nulo/vazio retorna mensagem")]
        [InlineData("")]
        [InlineData(null)]
        public void Usuario_ValidarFotoNuloVazio_DeveRetornarMensagem(string cpfInvalido)
        {
            //Arrange            
            var usuario = UsuarioBuilder.Novo().ComFoto(cpfInvalido).Buid();

            //Act
            var result = usuario.EhValido();

            //Assert            
            Assert.True(result);
            Assert.DoesNotContain(usuario?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("Foto do usuário inválida."));

        }

        [Trait("Categoria", "Usuario Bogus Testes")]
        [Theory(DisplayName = "Novo Usuario Nome Nulo/Vazio retorna excecao")]
        [InlineData("")]
        [InlineData(null)]
        public void Usuario_ValidarNomeNuloVazio_DeveRetornarExcecao(string nomeInvalido)
        {
            //Arrange
            var usuario = UsuarioBuilder.Novo().ComNome(nomeInvalido).Buid();

            //Assert & //Act
            var validationException = Assert.Throws<ValidationException>(() => usuario.Validar());
            Assert.Contains(validationException.Errors, f => f.ErrorMessage.Contains("Nome é obrigatório."));
        }

        [Trait("Categoria", "Usuario Bogus Testes")]
        [Theory(DisplayName = "Novo Usuario Email Nulo/Vazio retorna excecao")]
        [InlineData("")]
        [InlineData(null)]
        public void Usuario_ValidarEmailNuloVazio_DeveRetornarExcecao(string emailInvalido)
        {
            //Arrange            
            var usuario = UsuarioBuilder.Novo().ComEmail(emailInvalido).Buid();

            //Assert & //Act
            var validationException = Assert.Throws<ValidationException>(() => usuario.Validar());
            Assert.Contains(validationException.Errors, f => f.ErrorMessage.Contains("E-mail é obrigatório."));
        }

        [Trait("Categoria", "Usuario Bogus Testes")]
        [Theory(DisplayName = "Novo Usuario Email Inválido retorna excecao")]
        [InlineData("teste.gmail.com")]
        public void Usuario_ValidarEmailInvalido_DeveRetornarExcecao(string emailInvalido)
        {
            //Arrange           
            var usuario = UsuarioBuilder.Novo().ComEmail(emailInvalido).Buid();

            //Assert & //Act
            var validationException = Assert.Throws<ValidationException>(() => usuario.Validar());
            Assert.Contains(validationException.Errors, f => f.ErrorMessage.Contains("E-mail inválido."));
        }

        [Trait("Categoria", "Usuario Bogus Testes")]
        [Theory(DisplayName = "Novo Usuario CPF Nulo/Vazio retorna excecao")]
        [InlineData("")]
        [InlineData(null)]
        public void Usuario_ValidarCpfNuloVazio_DeveRetornarExcecao(string cpfInvalido)
        {
            //Arrange
            var usuario = UsuarioBuilder.Novo().ComCpf(cpfInvalido).Buid();

            //Assert & //Act
            var validationException = Assert.Throws<ValidationException>(() => usuario.Validar());
            Assert.Contains(validationException.Errors, f => f.ErrorMessage.Contains("Documento é obrigatório."));
        }

        [Trait("Categoria", "Usuario Bogus Testes")]
        [Theory(DisplayName = "Novo Usuario CPF Invalido retorna excecao")]
        [InlineData("19100000001")]
        [InlineData("1910000000")]
        [InlineData("191000000000")]
        [InlineData("11111111111")]
        [InlineData("1234567890")]
        public void Usuario_ValidarCpfInvalido_DeveRetornarExcecao(string cpfInvalido)
        {
            //Arrange
            var usuario = UsuarioBuilder.Novo().ComCpf(cpfInvalido).Buid();

            //Assert & //Act
            var validationException = Assert.Throws<ValidationException>(() => usuario.Validar());
            Assert.Contains(validationException.Errors, f => f.ErrorMessage.Contains("Documento inválido."));
        }
        
    }
}
