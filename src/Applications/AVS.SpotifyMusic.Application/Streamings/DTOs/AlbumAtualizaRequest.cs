namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class AlbumAtualizaRequest
	{        
        public string Titulo { get; set; }
		public string Descricao { get; set; }
		public string? Foto { get; set; }
        public Guid BandaId { get; set; }
		//public MusicaRequest Musica { get; set; }
        public ICollection<MusicaRequest> Musicas { get; set; } = new List<MusicaRequest>();

        public AlbumAtualizaRequest() { }
	}
}
