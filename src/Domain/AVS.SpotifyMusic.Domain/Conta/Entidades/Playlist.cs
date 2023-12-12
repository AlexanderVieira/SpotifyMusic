using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;

namespace AVS.SpotifyMusic.Domain.Conta.Entidades
{
    public class Playlist : Entity
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Foto { get; private set; }
        public bool Publica { get; private set; }
        public Usuario Usuario { get; private set; }
        public List<Musica> Musicas { get; private set; } = new List<Musica>();

        public Playlist(string titulo, string descricao, string foto, bool publica, Usuario usuario)
        {
            Titulo = titulo;
            Descricao = descricao;
            Foto = foto;
            Publica = publica;
            Usuario = usuario;
        }
    }
}
