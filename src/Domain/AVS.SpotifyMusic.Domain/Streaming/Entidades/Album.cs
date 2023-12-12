using AVS.SpotifyMusic.Domain.Core.ObjDomain;

namespace AVS.SpotifyMusic.Domain.Streaming.Entidades
{
    public class Album : Entity
    {
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public string Foto { get; private set; }
        public List<Musica> Musicas { get; private set; } = new List<Musica>();

        public Album(string titulo, string descricao, string foto)
        {
            Titulo = titulo;
            Descricao = descricao;
            Foto = foto;
        }
    }
}
