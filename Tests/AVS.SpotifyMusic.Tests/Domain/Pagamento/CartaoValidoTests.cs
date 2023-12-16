using AVS.SpotifyMusic.Domain.Transacao.Entidades;
using AVS.SpotifyMusic.Domain.Transacao.Enums;
using AVS.SpotifyMusic.Tests.Fixtures;
using FluentAssertions;

namespace AVS.SpotifyMusic.Tests.Domain.Pagamento
{
    [Collection(nameof(CartaoCollection))]
    public class CartaoValidoTests
    {
        private readonly CartaoFixtureTests _fixture;

        public CartaoValidoTests(CartaoFixtureTests fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Novo Cartao")]
        [Trait("Categoria", "Cartao Bogus Testes")]
        public void Cartao_CriarInstancia_DeveEstarValido()
        {
            //Arrange
            var cartao = _fixture.CriarCartaoValido();

            //Act
            var result = cartao.EhValido();

            //Assert            
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Cartao Ativo")]
        [Trait("Categoria", "Cartao Bogus Testes")]
        public void Cartao_Ativar_DeveTerFlagIgualVerdadeiro()
        {
            //Arrange
            var cartao = _fixture.CriarCartaoValido();

            //Act
            cartao?.Ativar();

            //Assert            
            Assert.True(cartao?.Ativo);
        }

        [Fact(DisplayName = "Cartao Inativo")]
        [Trait("Categoria", "Cartao Bogus Testes")]
        public void Cartao_Inativar_DeveTerFlagIgualFalso()
        {
            //Arrange
            var cartao = _fixture.CriarCartaoValido();

            //Act
            cartao?.Inativar();

            //Assert            
            Assert.False(cartao?.Ativo);
        }

        [Fact(DisplayName = "Cartao Atualizar Limite")]
        [Trait("Categoria", "Cartao Bogus Testes")]
        public void Cartao_AtualizarLimite_DeveSubtrairDoLimiteAtual()
        {
            //Arrange
            var cartao = _fixture.CriarCartaoValido();
            var transacao = new Transacao(DateTime.Now, 99.00M, "Lojas Novo Mundo", StatusTransacao.Pago);
            var limiteEsperado = cartao.Limite - transacao.Valor;
            
            //Act
            cartao?.AtualizarLimite(transacao);

            //Assert            
            Assert.Equal(limiteEsperado, cartao?.Limite.Valor);
        }

        [Fact(DisplayName = "Cartao Verificar Limite Disponível")]
        [Trait("Categoria", "Cartao Bogus Testes")]
        public void Cartao_VerificarLimite_DeveSerMaiorOuIgualValorDaTransacao()
        {
            //Arrange
            var cartao = _fixture.CriarCartaoValido();
            var transacao = new Transacao(DateTime.Now, 500M, "Lojas Novo Mundo", StatusTransacao.Pago);
            
            //Act
            var temLimite = cartao?.TemLimite(transacao);

            //Assert            
            temLimite.Should().BeTrue();
        }

        [Fact(DisplayName = "Cartao Verificar Limite Insuficiente")]
        [Trait("Categoria", "Cartao Bogus Testes")]
        public void Cartao_VerificarLimite_DeveSerMenorQueValorDaTransacao()
        {
            //Arrange
            var cartao = _fixture.CriarCartaoValido();
            var transacao = new Transacao(DateTime.Now, 15000.00M, "Lojas Novo Mundo", StatusTransacao.Pago);

            //Act
            var temLimite = cartao?.TemLimite(transacao);

            //Assert            
            temLimite.Should().BeFalse();
        }

        [Fact(DisplayName = "Cartao Adicionar Transacao")]
        [Trait("Categoria", "Cartao Bogus Testes")]
        public void Cartao_AdicionarTransacao_DeveTerTamanoMaiorQueZero()
        {
            //Arrange
            var cartao = _fixture.CriarCartaoValido();
            var transacao = new Transacao(DateTime.Now, 99.00M, "Lojas Novo Mundo", StatusTransacao.Pago);            
            var tamanhoEsperado = cartao.Transacoes.Count + 1;
            
            //Act
            cartao?.AdicionarTransacao(transacao);

            //Assert            
            Assert.Equal(tamanhoEsperado, cartao?.Transacoes.Count);
        }
       
    }
}
