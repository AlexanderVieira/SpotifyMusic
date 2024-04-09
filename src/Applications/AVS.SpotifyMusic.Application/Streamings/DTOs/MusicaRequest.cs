using AVS.SpotifyMusic.Application.Contas.DTOs;

namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class MusicaRequest
    {        
        public string Nome { get; set; }
		public int Duracao { get; set; }		

		public MusicaRequest() { }
	}
}
