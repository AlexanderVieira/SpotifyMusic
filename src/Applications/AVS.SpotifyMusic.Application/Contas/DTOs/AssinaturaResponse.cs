using AVS.SpotifyMusic.Application.Streamings.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVS.SpotifyMusic.Application.Contas.DTOs
{
	public class AssinaturaResponse
	{
        public Guid Id { get; set; }
        public bool Ativo { get; set; }
		public PlanoResponse Plano { get; set; }

		public AssinaturaResponse() { }
	}
}
