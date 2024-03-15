namespace AVS.SpotifyMusic.Domain.Core.ObjDomain
{
    public class UsuarioConsultaAnonima
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }             
        public string? Foto { get; set; }
    }
}
