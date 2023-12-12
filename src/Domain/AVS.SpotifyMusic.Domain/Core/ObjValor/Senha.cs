using AVS.SpotifyMusic.Domain.Core.ObjDomain;

namespace AVS.SpotifyMusic.Domain.Core.ObjValor
{
    public class Senha
    {
        public string Valor { get; private set; }

        public Senha(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor)) throw new DomainException("A senha deve ser informada");
            Valor = valor;
        }
    }
}
