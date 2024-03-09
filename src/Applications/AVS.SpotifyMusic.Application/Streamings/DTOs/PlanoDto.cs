using AVS.SpotifyMusic.Application.Contas.DTOs;

namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class PlanoDto
	{
		public string Nome { get; private set; }
		public string Descricao { get; private set; }
		public decimal Valor { get; private set; }
		public int TipoPlano { get; private set; }
		public AssinaturaDto Assinatura { get; set; }
		public Guid AssinaturaId { get; set; }
	}
}
