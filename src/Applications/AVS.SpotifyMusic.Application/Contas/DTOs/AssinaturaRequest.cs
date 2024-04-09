using AVS.SpotifyMusic.Application.Streamings.DTOs;

namespace AVS.SpotifyMusic.Application.Contas.DTOs
{
    public class AssinaturaRequest
	{
		public bool Ativo { get; set; }
		public PlanoRequest Plano { get; set; }
	}
}
