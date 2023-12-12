using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Core.ObjValor;

namespace AVS.SpotifyMusic.Domain.Transacao.Entidades
{
    public class Cartao : Entity
    {
        private const int INTERVALO_TRANSACAO = -2;
        private const int REPETICAO_TRANSACAO_MERCHANT = 1;

        public string Numero { get; private set; }
        public string Nome { get; private set; }
        public string Expiracao { get; private set; }
        public string Cvv { get; private set; }
        public bool Ativo { get; private set; }
        public Monetario Limite { get; private set; }
        public List<Transacao> Transacoes { get; private set; } = new List<Transacao>();

        public Cartao(string numero, string nome, string expiracao, string cvv, bool ativo, Monetario limite)
        {
            Numero = numero;
            Nome = nome;
            Expiracao = expiracao;
            Cvv = cvv;
            Ativo = ativo;
            Limite = limite;
        }
    }
}
