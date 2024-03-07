using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVS.SpotifyMusic.Application.Pagamentos.DTOs
{
	public class PagamentoDto
	{
		public decimal Valor { get; private set; }
		public int Situacao { get; private set; }
		public CartaoDto Cartao { get; private set; }
		public TransacaoDto Transacao { get; private set; }
	}
}
