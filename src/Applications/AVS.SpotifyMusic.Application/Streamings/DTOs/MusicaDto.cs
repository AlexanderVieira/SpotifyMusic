using AVS.SpotifyMusic.Application.Contas.DTOs;

namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class MusicaDto
	{
		public string Nome { get; set; }
		public int Duracao { get; set; }
		public List<PlaylistDto> Playlists { get; set; } = new List<PlaylistDto>();
	}
}
