using AVS.SpotifyMusic.Domain.Streaming.Entidades;

namespace AVS.SpotifyMusic.Domain.Streaming.Factories
{
    public static class AlbumFactory
    {
        public static Album? Criar(string titulo, string descricao, string foto, Musica musica)
        {
            if (!musica.EhValido()) return null;
            var album = new Album(titulo, descricao, foto);
            album.Musicas.Add(musica);
            return album;
        }
    }
}
