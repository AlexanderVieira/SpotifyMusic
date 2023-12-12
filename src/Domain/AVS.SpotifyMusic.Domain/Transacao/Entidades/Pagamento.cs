using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Core.ObjValor;
using AVS.SpotifyMusic.Domain.Transacao.Enums;

namespace AVS.SpotifyMusic.Domain.Transacao.Entidades
{
    public class Pagamento : Entity
    {
        public Monetario Valor { get; private set; }
        public StatusPagamento Situacao { get; private set; }
        public Cartao Cartao { get; private set; }
        public Transacao Transacao { get; private set; }

        public Pagamento(decimal valor, StatusPagamento situacao, Cartao cartao, Transacao transacao)
        {
            Valor = new Monetario(valor);
            Situacao = situacao;
            Cartao = cartao;
            Transacao = transacao;
        }
    }
}
