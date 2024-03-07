using AVS.SpotifyMusic.Application.Streamings.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVS.SpotifyMusic.Application.Contas.DTOs
{
	public class AssinaturaDto
	{
		public bool Ativo { get; private set; }
		public PlanoDto Plano { get; private set; }
	}
}
