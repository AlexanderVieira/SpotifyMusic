namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class BandaDto
	{
		public string Nome { get; private set; }
		public string Descricao { get; private set; }
		public string? Foto { get; private set; }
		public List<AlbumDto> Albuns { get; private set; } = new List<AlbumDto>();
	}
}
