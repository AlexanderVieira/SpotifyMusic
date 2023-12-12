using AVS.SpotifyMusic.Domain.Core.ObjDomain;
using AVS.SpotifyMusic.Domain.Core.ObjValor;
using AVS.SpotifyMusic.Domain.Transacao.Entidades;

namespace AVS.SpotifyMusic.Domain.Conta.Entidades
{
    public class Usuario : Entity
    {
        public string Nome { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public Senha Senha { get; private set; }
        public string Foto { get; private set; }
        public bool Ativo { get; set; }
        public DateTime DtNascimento { get; private set; }
        public List<Cartao> Cartoes { get; private set; } = new List<Cartao>();
        public List<Assinatura> Assinaturas { get; private set; } = new List<Assinatura>();
        public List<Playlist> Playlists { get; private set; } = new List<Playlist>();

        public Usuario(string nome, string email, string cpf, string senha, string foto, bool ativo, DateTime dtNascimento)
        {
            Nome = nome;
            Email = new Email(email);
            Cpf = new Cpf(cpf);
            Senha = new Senha(senha);
            Foto = foto;
            Ativo = ativo;
            DtNascimento = dtNascimento;
        }
    }
}
