using AVS.SpotifyMusic.Application.Streamings.DTOs;

namespace AVS.SpotifyMusic.Application.Contas.DTOs
{
    public class PlaylistResponse
	{
        public Guid Id { get; set; }
        public string Titulo { get; set; }
		public string Descricao { get; set; }
		public string? Foto { get; set; }
		public bool Publica { get; set; }			
		public UsuarioResponse Usuario { get; set; }
		public ICollection<MusicaResponse> Musicas { get; set; } = new List<MusicaResponse>();

        public PlaylistResponse()
        {            
        }
    }
}
