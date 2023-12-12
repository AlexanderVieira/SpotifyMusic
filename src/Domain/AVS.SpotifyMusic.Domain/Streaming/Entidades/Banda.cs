using AVS.SpotifyMusic.Domain.Core.ObjDomain;

namespace AVS.SpotifyMusic.Domain.Streaming.Entidades
{
    public class Banda : Entity
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public string Foto { get; private set; }
        public List<Album> Albuns { get; private set; } = new List<Album>();

        public Banda(string nome, string descricao, string foto)
        {
            Nome = nome;
            Descricao = descricao;
            Foto = foto;
        }
    }
}
