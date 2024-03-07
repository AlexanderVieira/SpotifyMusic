using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVS.SpotifyMusic.Application.Pagamentos.DTOs
{
	public class CartaoDto
	{
		public string Numero { get; private set; }
		public string Nome { get; private set; }
		public string Expiracao { get; private set; }
		public string Cvv { get; private set; }
		public bool Ativo { get; private set; }
		public decimal Limite { get; private set; }
		public PagamentoDto Pagamento { get; set; }
		public Guid PagamentoId { get; set; }
		public List<TransacaoDto> Transacoes { get; private set; } = new List<TransacaoDto>();
	}
}
