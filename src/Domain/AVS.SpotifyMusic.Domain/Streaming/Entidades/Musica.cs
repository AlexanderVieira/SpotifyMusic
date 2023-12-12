using AVS.SpotifyMusic.Domain.Conta.Entidades;
using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Streaming.ObjValor;

namespace AVS.SpotifyMusic.Domain.Streaming.Entidades
{
    public class Musica : Entity
    {
        public string Nome { get; private set; }
        public Duracao Duracao { get; private set; }
        public List<Playlist> Playlists { get; private set; } = new List<Playlist>();

        public Musica(string nome, int duracao)
        {
            Nome = nome;
            Duracao = new Duracao(duracao);
        }
    }
}
