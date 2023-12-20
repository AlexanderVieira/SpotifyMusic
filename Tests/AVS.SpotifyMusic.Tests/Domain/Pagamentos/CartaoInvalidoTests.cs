using AVS.SpotifyMusic.Domain.Pagamentos.Entidades;
using AVS.SpotifyMusic.Tests.Builders;
using AVS.SpotifyMusic.Tests.Fixtures;
using FluentAssertions;

namespace AVS.SpotifyMusic.Tests.Domain.Pagamentos
{
    [Collection(nameof(CartaoCollection))]
    public class CartaoInvalidoTests
    {
        private readonly CartaoFixtureTests _fixture;

        public CartaoInvalidoTests(CartaoFixtureTests fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Novo Cartao Inválido")]
        [Trait("Categoria", "Cartao Bogus Testes")]
        public void Cartao_CriarInstancia_DeveEstarInvalido()
        {
            //Arrange
            Cartao? cartao = _fixture.CriarCartaoInvalido();

            //Act
            var result = cartao.EhValido();

            //Assert
            result.Should().BeFalse();
        }

        [Trait("Categoria", "Cartao Bogus Testes")]
        [Theory(DisplayName = "Novo Cartao Nome nulo/vazio retorna mensagem")]
        [InlineData("")]
        [InlineData(null)]
        public void Cartao_ValidarNomeNuloVazio_DeveRetornarMensagem(string nomeInvalido)
        {
            //Arrange            
            var cartao = CartaoBuilder.Novo().ComNome(nomeInvalido).Buid();

            //Act
            var result = cartao.EhValido();

            //Assert            
            Assert.False(result);
            Assert.Contains(cartao?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("Nome é obrigatório."));           

        }

        [Trait("Categoria", "Cartao Bogus Testes")]
        [Theory(DisplayName = "Novo Cartao Nome inválido retorna mensagem")]
        [InlineData("Emily")]
        [InlineData("Alex")]
        [InlineData("Antonella Débora Bárbara Silveira")]
        [InlineData("Patrícia I Heloisa Viana Cavalcanti")]
        public void Cartao_ValidarNomeInvalido_DeveRetornarMensagem(string nomeInvalido)
        {
            //Arrange            
            var cartao = CartaoBuilder.Novo().ComNome(nomeInvalido).Buid();

            //Act
            var result = cartao.EhValido();

            //Assert            
            Assert.False(result);
            Assert.Contains(cartao?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("O Nome deve ter entre 6 a 30 caracteres."));

        }

        [Trait("Categoria", "Cartao Bogus Testes")]
        [Theory(DisplayName = "Novo Cartao Número nulo/vazio retorna mensagem")]
        [InlineData("")]
        [InlineData(null)]
        public void Cartao_ValidarNumeroNuloVazio_DeveRetornarMensagem(string numeroInvalido)
        {
            //Arrange            
            var cartao = CartaoBuilder.Novo().ComNumero(numeroInvalido).Buid();

            //Act
            var result = cartao.EhValido();

            //Assert            
            Assert.False(result);
            Assert.Contains(cartao?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("Número é obrigatório."));

        }

        [Trait("Categoria", "Cartao Bogus Testes")]
        [Theory(DisplayName = "Novo Cartao Número inválido retorna mensagem")]        
        [InlineData("5464-1733-1700-65521")]
        [InlineData("5464-1733-1700-655")]
        [InlineData("5464173317006552")]
        public void Cartao_ValidarNumeroInvalido_DeveRetornarMensagem(string numeroInvalido)
        {
            //Arrange            
            var cartao = CartaoBuilder.Novo().ComNumero(numeroInvalido).Buid();

            //Act
            var result = cartao.EhValido();

            //Assert            
            Assert.False(result);
            Assert.Contains(cartao?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("Número inválido."));

        }

        [Trait("Categoria", "Cartao Bogus Testes")]
        [Theory(DisplayName = "Novo Cartao Expiracao nulo/vazio retorna mensagem")]
        [InlineData("")]        
        [InlineData(null)]
        public void Cartao_ValidarExpiracaoNuloVazio_DeveRetornarMensagem(string expiracaoInvalido)
        {
            //Arrange            
            var cartao = CartaoBuilder.Novo().ComExpiracao(expiracaoInvalido).Buid();

            //Act
            var result = cartao.EhValido();

            //Assert            
            Assert.False(result);
            Assert.Contains(cartao?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("Data de Expiração é obrigatória."));

        }

        [Trait("Categoria", "Cartao Bogus Testes")]
        [Theory(DisplayName = "Novo Cartao Expiracao inválido retorna mensagem")]
        [InlineData("1/2024")]
        [InlineData("01/25")]
        public void Cartao_ValidarExpiracaoInvalida_DeveRetornarMensagem(string expiracaoInvalido)
        {
            //Arrange            
            var cartao = CartaoBuilder.Novo().ComExpiracao(expiracaoInvalido).Buid();

            //Act
            var result = cartao.EhValido();

            //Assert            
            Assert.False(result);
            Assert.Contains(cartao?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("Data de Expiração inválida."));

        }

        [Trait("Categoria", "Cartao Bogus Testes")]
        [Theory(DisplayName = "Novo Cartao Cvv nulo/vazio retorna mensagem")]
        [InlineData("")]
        [InlineData(null)]
        public void Cartao_ValidarCvvNuloVazio_DeveRetornarMensagem(string cvvInvalido)
        {
            //Arrange            
            var cartao = CartaoBuilder.Novo().ComCvv(cvvInvalido).Buid();

            //Act
            var result = cartao.EhValido();

            //Assert            
            Assert.False(result);
            Assert.Contains(cartao?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("Cvv é obrigatório."));

        }

        [Trait("Categoria", "Cartao Bogus Testes")]
        [Theory(DisplayName = "Novo Cartao Cvv inválido retorna mensagem")]
        [InlineData("19")]
        [InlineData("1956")]
        public void Cartao_ValidarCvvInvalido_DeveRetornarMensagem(string cvvInvalido)
        {
            //Arrange            
            var cartao = CartaoBuilder.Novo().ComCvv(cvvInvalido).Buid();

            //Act
            var result = cartao.EhValido();

            //Assert            
            Assert.False(result);
            Assert.Contains(cartao?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("Cvv inválido."));

        }

        [Trait("Categoria", "Cartao Bogus Testes")]
        [Theory(DisplayName = "Novo Cartao Limite menor que zero retorna mensagem")]        
        [InlineData(-1)]
        public void Cartao_ValidarLimiteInvalido_DeveRetornarMensagem(decimal limiteInvalido)
        {
            //Arrange            
            var cartao = CartaoBuilder.Novo().ComLimite(limiteInvalido).Buid();

            //Act
            var result = cartao.EhValido();

            //Assert            
            Assert.False(result);
            Assert.Contains(cartao?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("O valor do limite não pode ser negativo."));

        }

        [Trait("Categoria", "Cartao Bogus Testes")]
        [Theory(DisplayName = "Cartao Inativo retorna mensagem")]
        [InlineData(false)]
        public void Cartao_ValidarInativo_DeveRetornarMensagem(bool inativo)
        {
            //Arrange            
            var cartao = CartaoBuilder.Novo().ComFlagAtivo(inativo).Buid();

            //Act
            var result = cartao.EhValido();

            //Assert            
            Assert.False(result);
            Assert.Contains(cartao?.ValidationResult?.Errors, f => f.ErrorMessage.Contains("Cartão não está ativo."));

        }

    }
}
