using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Core.ObjValor;
using AVS.SpotifyMusic.Domain.Transacao.Enums;
using AVS.SpotifyMusic.Domain.Transacao.ObjValor;

namespace AVS.SpotifyMusic.Domain.Transacao.Entidades
{
    public class Transacao : Entity
    {
        public DateTime DtTransacao { get; private set; }
        public Monetario Valor { get; private set; }
        public Merchant Merchant { get; private set; }
        public string Descricao { get; private set; }
        public StatusTransacao Situacao { get; private set; }
        //public Pagamento? Pagamento { get; set; }

        public Transacao(DateTime dtTransacao, decimal valor, string merchantName, StatusTransacao situacao, string descricao = "")
        {
            DtTransacao = dtTransacao;
            Valor = new Monetario(valor);
            Merchant = new Merchant(merchantName);
            Descricao = descricao;
            Situacao = situacao;
        }
    }
}
