using AVS.SpotifyMusic.Application.Contas.DTOs;

namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class PlanoResponse
	{
        public Guid Id { get; set; }
        public string Nome { get; set; }
		public string Descricao { get; set; }
		public decimal Valor { get; set; }
		public int TipoPlano { get; set; }
		public AssinaturaResponse Assinatura { get; set; }
		public Guid AssinaturaId { get; set; }

        public PlanoResponse()
        {            
        }
    }
}
