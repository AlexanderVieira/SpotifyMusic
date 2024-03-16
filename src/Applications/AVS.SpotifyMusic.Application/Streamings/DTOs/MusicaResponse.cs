using AVS.SpotifyMusic.Application.Contas.DTOs;

namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class MusicaResponse
	{
        public Guid Id { get; set; }
        public string Nome { get; set; }
		public int Duracao { get; set; }
		public ICollection<PlaylistResponse> Playlists { get; set; } = new List<PlaylistResponse>();

		public MusicaResponse() { }
	}
}
