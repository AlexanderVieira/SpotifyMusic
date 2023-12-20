using AVS.SpotifyMusic.Domain.Core.ObjValor;
using AVS.SpotifyMusic.Domain.Pagamentos.Entidades;
using AVS.SpotifyMusic.Tests.Fixtures;

namespace AVS.SpotifyMusic.Tests.Builders
{
    [Collection(nameof(CartaoCollection))]
    public class CartaoBuilder
    {
        public string Numero { get; private set; }
        public string Nome { get; private set; }
        public string Expiracao { get; private set; }
        public string Cvv { get; private set; }
        public bool Ativo { get; private set; }
        public Monetario Limite { get; private set; }        

        public CartaoBuilder()
        {
            var fixture = new CartaoFixtureTests();
            var cartao =  fixture.CriarCartaoValido();   
            Numero = cartao.Numero;
            Nome = cartao.Nome;
            Expiracao = cartao.Expiracao;
            Cvv = cartao.Cvv;
            Ativo = cartao.Ativo;
            Limite = cartao.Limite;            
        }

        public static CartaoBuilder Novo()
        {
            return new CartaoBuilder();
        }        

        public CartaoBuilder ComNome(string nome)
        {
            Nome = nome;
            return this;
        }

        public CartaoBuilder ComNumero(string numero)
        {
            Numero = numero;
            return this;
        }

        public CartaoBuilder ComExpiracao(string expiracao)
        {
            Expiracao = expiracao;
            return this;
        }

        public CartaoBuilder ComCvv(string cvv)
        {
            Cvv = cvv;
            return this;
        }

        public CartaoBuilder ComFlagAtivo(bool ativo)
        {
            Ativo = ativo;
            return this;
        }

        public CartaoBuilder ComLimite(decimal limite)
        {
            Limite = new Monetario(limite);
            return this;
        }        

        public Cartao Buid()
        {
            return new Cartao(Numero, Nome, Expiracao, Cvv, Ativo, Limite.Valor);
        }
    }
}
