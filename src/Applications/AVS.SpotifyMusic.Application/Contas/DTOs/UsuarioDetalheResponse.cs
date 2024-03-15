using AVS.SpotifyMusic.Application.Pagamentos.DTOs;

namespace AVS.SpotifyMusic.Application.Contas.DTOs
{
    public class UsuarioDetalheResponse
	{
        public Guid Id { get; set; }
        public string Nome { get; set; }
		public string Email { get; set; }
		public string Cpf { get; set; }
		public string Senha { get; set; }
		public string? Foto { get; set; }
		public bool Ativo { get; set; }
		public DateTime DtNascimento { get; set; }
		public ICollection<CartaoResponse> Cartoes { get; set; } = new List<CartaoResponse>();
		public ICollection<AssinaturaResponse> Assinaturas { get; set; } = new List<AssinaturaResponse>();
		public ICollection<PlaylistResponse> Playlists { get; set; } = new List<PlaylistResponse>();

		public UsuarioDetalheResponse() { }

	}
}
