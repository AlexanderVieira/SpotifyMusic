using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVS.SpotifyMusic.Application.Pagamentos.DTOs
{
	public class PagamentoResponse
	{
        public Guid Id { get; set; }
        public decimal Valor { get; set; }
		public int Situacao { get; set; }
		public CartaoResponse Cartao { get; set; }
		public TransacaoResponse Transacao { get; set; }

        public PagamentoResponse()
        {            
        }
    }
}
