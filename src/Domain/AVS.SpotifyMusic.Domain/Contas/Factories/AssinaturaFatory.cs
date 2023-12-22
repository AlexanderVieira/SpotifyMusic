using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;

namespace AVS.SpotifyMusic.Domain.Conta.Factories
{
    public static class AssinaturaFatory
    {
        public static Assinatura Criar(Plano plano, bool ativo)
        {
            var assinatura = new Assinatura(plano, ativo);            
            return assinatura;
        }
    }
}
