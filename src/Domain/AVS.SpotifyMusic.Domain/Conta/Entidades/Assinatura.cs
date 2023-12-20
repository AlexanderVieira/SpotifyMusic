using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;

namespace AVS.SpotifyMusic.Domain.Conta.Entidades
{
    public class Assinatura : Entity
    {
        public bool Ativo { get; private set; }
        public Plano Plano { get; private set; }        

        protected Assinatura()
        {            
        }

        public Assinatura(Plano plano, bool ativo)
        {
            Plano = plano;
            Ativo = ativo;
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Inativar()
        {
            Ativo = false;
        }

        public void AtualizarPlano(Plano plano) 
        {
            Plano = plano;
        }
    }
}
