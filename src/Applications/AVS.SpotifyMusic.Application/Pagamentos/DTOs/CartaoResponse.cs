namespace AVS.SpotifyMusic.Application.Pagamentos.DTOs
{
    public class CartaoResponse
	{
        public Guid Id { get; set; }
        public string Numero { get; set; }
		public string Nome { get; set; }
		public string Expiracao { get; set; }
		public string Cvv { get; set; }
		public bool Ativo { get; set; }
		public decimal Limite { get; set; }
		public PagamentoResponse Pagamento { get; set; }
		public Guid PagamentoId { get; set; }
		public ICollection<TransacaoResponse> Transacoes { get; set; } = new List<TransacaoResponse>();

	}
}
