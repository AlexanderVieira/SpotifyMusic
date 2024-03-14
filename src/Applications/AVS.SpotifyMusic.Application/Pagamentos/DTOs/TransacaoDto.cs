namespace AVS.SpotifyMusic.Application.Pagamentos.DTOs
{
	public class TransacaoDto
	{
		public decimal Valor { get; set; }
		public string Merchant { get; set; }
		public string? Descricao { get; set; }
		public int Situacao { get; set; }
		public PagamentoDto Pagamento { get; set; }
		public Guid PagamentoId { get; set; }
	}
}
