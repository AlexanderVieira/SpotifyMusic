using AVS.SpotifyMusic.Domain.Pagamentos.Entidades;
using AVS.SpotifyMusic.Domain.Pagamentos.Enums;
using FluentAssertions;

namespace AVS.SpotifyMusic.Tests.Domain.Pagamentos
{
    public class PagamentoValidoTests
    {
        public PagamentoValidoTests()
        {
        }

        [Fact(DisplayName = "Novo Pagamento Instaância Válida")] 
        [Trait("Categoria", "Pagamento Bogus Testes")]
        public void Pagamento_CriarInstancia_DeveEstarValido()
        {
            //Arrange
            var pagamento = new Pagamento(590.00M,
                StatusPagamento.Pago,
                new Cartao("5464-1733-1700-6552", "Patrícia I Heloisa Viana", "05/26", "198", true, 5000.00M),
                new Transacao(590.00M, "Lojas Novo Mundo", StatusTransacao.Pago));

            //Act
            var result = pagamento.EhValido();

            //Assert            
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Novo Pagamento Limite Transacoes Excedido")]
        [Trait("Categoria", "Pagamento Bogus Testes")]
        public void Pagamento_ValidarLimiteTransacoesExcedido_DeveRetornarMensagem()
        {
            //Arrange
            var pagamento = new Pagamento(1290.00M,
                StatusPagamento.Cancelado,
                new Cartao("5464-1733-1700-6552", "Patrícia I Heloisa Viana", "05/26", "198", true, 2000.00M),
                new Transacao(1290.00M, "Lojas Novo Mundo", StatusTransacao.Recusado));

            //Act            
            pagamento.ValidarLimiteTransacoesExecedido(4);

            //Assert            
            Assert.Contains(pagamento.ValidationResult?.Errors, f => f.ErrorMessage.Contains("Cartão utilizado muitas vezes em um período curto."));
        }

        [Fact(DisplayName = "Novo Pagamento Transacao Repetida por Comerciante")]
        [Trait("Categoria", "Pagamento Bogus Testes")]
        public void Pagamento_ValidarTransacaoRepetidaPorMerchant_DeveRetornarMensagem()
        {
            //Arrange
            var pagamento = new Pagamento(1290.00M,
                StatusPagamento.Pago,
                new Cartao("5464-1733-1700-6552", "Patrícia I Heloisa Viana", "05/26", "198", true, 2000.00M),
                new Transacao(1290.00M, "Lojas Novo Mundo", StatusTransacao.Pago));

            pagamento.CriarTransacao(pagamento.Cartao, pagamento.Transacao);            

            //Act            
            pagamento.ValidarTransacaoRepetidaPorMerchant(pagamento.Cartao.Transacoes, new Transacao(1290.00M, "Lojas Novo Mundo", StatusTransacao.Recusado));

            //Assert            
            Assert.Contains(pagamento.ValidationResult?.Errors, f => f.ErrorMessage.Contains("Transacao Duplicada para o mesmo cartão e o mesmo Comerciante."));
        }

        [Fact(DisplayName = "Novo Pagamento Verificar Limite Cartao")]
        [Trait("Categoria", "Pagamento Bogus Testes")]
        public void Pagamento_VerificarLimiteCartao_DeveRetornarMensagem()
        {
            //Arrange
            var pagamento = new Pagamento(1290.00M,
                StatusPagamento.Pago,
                new Cartao("5464-1733-1700-6552", "Patrícia I. Heloisa Viana", "05/26", "198", true, 2000.00M),
                new Transacao(1290.00M, "Lojas Novo Mundo", StatusTransacao.Pago));

            pagamento.CriarTransacao(pagamento.Cartao, pagamento.Transacao);            

            //Act            
            pagamento.VerificarLimiteCartao(pagamento.Cartao, new Transacao(1290.00M, "Lojas Novo Mundo", StatusTransacao.Recusado));

            //Assert            
            Assert.Contains(pagamento.ValidationResult?.Errors, f => f.ErrorMessage.Contains("Cartão não possui limite para esta transação."));
        }
    }
}
