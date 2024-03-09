namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class AlbumDto
	{
		public string Titulo { get; private set; }
		public string Descricao { get; private set; }
		public string? Foto { get; private set; }
		public List<MusicaDto> Musicas { get; private set; } = new List<MusicaDto>();
	}
}
