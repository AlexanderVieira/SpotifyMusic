using AVS.SpotifyMusic.Domain.Core.Utils;
using AVS.SpotifyMusic.Domain.Streaming.Entidades;

namespace AVS.SpotifyMusic.Domain.Streaming.Factories
{
    public static class AlbumFactory
    {
        public static Album Criar(string titulo, string descricao, string foto, Musica musica)
        {
            ValidarMusica(musica);
            if (!musica.EhValido()) return null;
            var album = new Album(titulo, descricao, foto);
            album.AdicionarMusica(musica);
            return album;
        }

        public static Album Criar(string titulo, string descricao, string? foto, ICollection<Musica> musicas)
        {
            ValidarMusicas(musicas);
            var album = new Album(titulo, descricao, foto);
            album.AtualizarMusicas(musicas);

            return album;
        }

        private static void ValidarMusica(Musica musica)
        {
            Validation.ValidarSeNulo(musica, "Para criar um album, o album deve ter no minimo uma musica");
        }

        private static void ValidarMusicas(ICollection<Musica> musicas)
        {
            var lista = musicas.Cast<object>().ToList(); ;
            Validation.ValidarSeExiste(lista, "Para criar um album, o album deve ter no minimo uma musica");
        }
    }
}
