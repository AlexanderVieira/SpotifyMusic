using AVS.SpotifyMusic.Application.Pagamentos.DTOs;

namespace AVS.SpotifyMusic.Application.Contas.DTOs
{
    public class UsuarioDto
	{
		public string Nome { get; set; }
		public string Email { get; set; }
		public string Cpf { get; set; }
		public string Senha { get; set; }
		public string? Foto { get; set; }
		public bool Ativo { get; set; }
		public DateTime DtNascimento { get; set; }
		public List<CartaoDto> Cartoes { get; private set; } = new List<CartaoDto>();
		public List<AssinaturaDto> Assinaturas { get; private set; } = new List<AssinaturaDto>();
		public List<PlaylistDto> Playlists { get; private set; } = new List<PlaylistDto>();

	}
}
