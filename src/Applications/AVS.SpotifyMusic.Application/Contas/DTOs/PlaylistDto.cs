using AVS.SpotifyMusic.Application.Streamings.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVS.SpotifyMusic.Application.Contas.DTOs
{
	public class PlaylistDto
	{
		public string Titulo { get; set; }
		public string Descricao { get; set; }
		public string? Foto { get; set; }
		public bool Publica { get; set; }
		public UsuarioDto Usuario { get; set; }
		public List<MusicaDto> Musicas { get; set; } = new List<MusicaDto>();
	}
}
