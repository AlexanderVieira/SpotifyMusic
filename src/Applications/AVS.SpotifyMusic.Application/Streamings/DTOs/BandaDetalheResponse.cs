namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class BandaDetalheResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
		public string Descricao { get; set; }
		public string? Foto { get; set; }		
		public ICollection<AlbumResponse> Albuns { get; set; } = new List<AlbumResponse>();
		
		public BandaDetalheResponse() { }
	}
}
