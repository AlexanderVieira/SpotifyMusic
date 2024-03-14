﻿using AVS.SpotifyMusic.Application.Contas.DTOs;

namespace AVS.SpotifyMusic.Application.Streamings.DTOs
{
	public class PlanoDto
	{
		public string Nome { get; set; }
		public string Descricao { get; set; }
		public decimal Valor { get; set; }
		public int TipoPlano { get; set; }
		//public AssinaturaDto Assinatura { get; set; }
		//public Guid AssinaturaId { get; set; }
	}
}
