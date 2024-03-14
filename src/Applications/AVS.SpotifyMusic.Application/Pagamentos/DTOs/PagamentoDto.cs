using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVS.SpotifyMusic.Application.Pagamentos.DTOs
{
	public class PagamentoDto
	{
		public decimal Valor { get; set; }
		public int Situacao { get; set; }
		public CartaoDto Cartao { get; set; }
		public TransacaoDto Transacao { get; set; }
	}
}
