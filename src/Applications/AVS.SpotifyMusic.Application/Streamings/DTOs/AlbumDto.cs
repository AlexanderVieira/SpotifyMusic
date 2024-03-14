namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class AlbumDto
	{
		public string Titulo { get; set; }
		public string Descricao { get; set; }
		public string? Foto { get; set; }
		public List<MusicaDto> Musicas { get; set; } = new List<MusicaDto>();
	}
}
