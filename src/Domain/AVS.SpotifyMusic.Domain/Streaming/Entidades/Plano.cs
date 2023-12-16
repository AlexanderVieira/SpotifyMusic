using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Core.ObjValor;
using AVS.SpotifyMusic.Domain.Streaming.Enums;

namespace AVS.SpotifyMusic.Domain.Streaming.Entidades
{
    public class Plano : Entity
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public Monetario Valor { get; private set; }
        public TipoPlano TipoPlano { get; private set; }

        public Plano(string nome, string descricao, decimal valor, TipoPlano tipoPlano)
        {
            Nome = nome;
            Descricao = descricao;
            Valor = new Monetario(valor);
            TipoPlano = tipoPlano;            
        }        
        
    }
}
