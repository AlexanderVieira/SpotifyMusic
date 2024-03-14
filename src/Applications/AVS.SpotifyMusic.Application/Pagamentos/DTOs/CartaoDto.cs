﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVS.SpotifyMusic.Application.Pagamentos.DTOs
{
	public class CartaoDto
	{
		public string Numero { get; set; }
		public string Nome { get; set; }
		public string Expiracao { get; set; }
		public string Cvv { get; set; }
		public bool Ativo { get; set; }
		public decimal Limite { get; set; }
		//public PagamentoDto Pagamento { get; set; }
		public Guid PagamentoId { get; set; }
		//public List<TransacaoDto> Transacoes { get; set; } = new List<TransacaoDto>();
	}
}
