using AVS.SpotifyMusic.Application.Pagamentos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVS.SpotifyMusic.Application.Contas.DTOs
{
	public class UsuarioDto
	{
		public string Nome { get; private set; }
		public string Email { get; private set; }
		public string Cpf { get; private set; }
		public string Senha { get; private set; }
		public string? Foto { get; private set; }
		public bool Ativo { get; set; }
		public DateTime DtNascimento { get; private set; }
		public List<CartaoDto> Cartoes { get; private set; } = new List<CartaoDto>();
		public List<AssinaturaDto> Assinaturas { get; private set; } = new List<AssinaturaDto>();
		public List<PlaylistDto> Playlists { get; private set; } = new List<PlaylistDto>();
		
	}
}
