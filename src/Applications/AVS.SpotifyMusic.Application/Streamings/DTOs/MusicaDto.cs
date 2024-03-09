using AVS.SpotifyMusic.Application.Contas.DTOs;

namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class MusicaDto
	{
		public string Nome { get; private set; }
		public int Duracao { get; private set; }
		public List<PlaylistDto> Playlists { get; private set; } = new List<PlaylistDto>();
	}
}
