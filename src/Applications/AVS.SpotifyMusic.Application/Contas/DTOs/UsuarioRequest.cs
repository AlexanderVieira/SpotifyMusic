using AVS.SpotifyMusic.Application.Pagamentos.DTOs;

namespace AVS.SpotifyMusic.Application.Contas.DTOs
{
    public class UsuarioRequest
	{
		public string Nome { get; set; }
		public string Email { get; set; }
		public string Cpf { get; set; }
		public string Senha { get; set; }
		public string? Foto { get; set; }
		public bool Ativo { get; set; }
		public DateTime DtNascimento { get; set; } 
        public CartaoRequest Cartao { get; set; }        
		public AssinaturaRequest Assinatura { get; set; }
        
    }
}
