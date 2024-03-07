using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVS.SpotifyMusic.Application.Pagamentos.DTOs
{
	public class TransacaoDto
	{
		public decimal Valor { get; private set; }
		public string Merchant { get; private set; }
		public string? Descricao { get; private set; }
		public int Situacao { get; private set; }
		public PagamentoDto Pagamento { get; set; }
		public Guid PagamentoId { get; set; }
	}
}
