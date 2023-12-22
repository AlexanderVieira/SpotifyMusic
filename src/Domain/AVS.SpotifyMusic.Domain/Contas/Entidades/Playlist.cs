using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;

namespace AVS.SpotifyMusic.Domain.Contas.Entidades
{
    public class Playlist : Entity
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string? Foto { get; private set; }
        public bool Publica { get; private set; }
        public Usuario Usuario { get; private set; }
        public List<Musica> Musicas { get; private set; } = new List<Musica>();

        protected Playlist()
        {            
        }

        public Playlist(string titulo, string descricao, bool publica, Usuario usuario, string? foto = null)
        {
            Titulo = titulo;
            Descricao = descricao;
            Foto = foto;
            Publica = publica;
            Usuario = usuario;
        }
    }
}
