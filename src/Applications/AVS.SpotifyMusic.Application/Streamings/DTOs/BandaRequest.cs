namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class BandaRequest
	{
        public Guid? Id { get; set; }
        public string Nome { get; set; }
		public string Descricao { get; set; }
		public string? Foto { get; set; }
		
		public BandaRequest() { }
	}
}
