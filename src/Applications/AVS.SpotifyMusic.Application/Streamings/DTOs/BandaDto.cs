namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class BandaDto
	{
		public string Nome { get; set; }
		public string Descricao { get; set; }
		public string? Foto { get; set; }
		public List<AlbumDto> Albuns { get; set; } = new List<AlbumDto>();
	}
}
