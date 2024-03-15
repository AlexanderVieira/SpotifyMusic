namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class BandaResponse
	{
        public Guid Id { get; set; }
        public string Nome { get; set; }
		public string Descricao { get; set; }
		public string? Foto { get; set; }
		public List<AlbumResponse> Albuns { get; set; } = new List<AlbumResponse>();
		public BandaResponse() { }
	}
}
