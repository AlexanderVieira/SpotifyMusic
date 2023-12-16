using AVS.SpotifyMusic.Domain.Conta.Entidades;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;
using AVS.SpotifyMusic.Domain.Streaming.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
