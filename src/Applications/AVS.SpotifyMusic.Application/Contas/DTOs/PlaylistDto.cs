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
		public string Titulo { get; private set; }
		public string Descricao { get; private set; }
		public string? Foto { get; private set; }
		public bool Publica { get; private set; }
		public UsuarioDto Usuario { get; private set; }
		public List<MusicaDto> Musicas { get; private set; } = new List<MusicaDto>();
	}
}
