namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class AlbumRequest
	{
		public Guid Id { get; set; }        
        public string Titulo { get; set; }
		public string Descricao { get; set; }
		public string? Foto { get; set; }
        public Guid BandaId { get; set; }		
        public ICollection<MusicaRequest> Musicas { get; set; } = new List<MusicaRequest>();

        public AlbumRequest() { }
	}
}
