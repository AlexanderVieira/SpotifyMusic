namespace AVS.SpotifyMusic.Application.Pagamentos.DTOs
{
	public class TransacaoResponse
	{
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
		public string Merchant { get; set; }
		public string? Descricao { get; set; }
		public int Situacao { get; set; }
		public PagamentoResponse Pagamento { get; set; }
		public Guid PagamentoId { get; set; }

        public TransacaoResponse()
        {            
        }
    }
}
