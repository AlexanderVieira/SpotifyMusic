using AVS.SpotifyMusic.Domain.Conta.Entidades;
using AVS.SpotifyMusic.Tests.Fixtures;

namespace AVS.SpotifyMusic.Tests.Builders
{
    [Collection(nameof(UsuarioCollection))]
    public class UsuarioBuilder
    {   
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public bool Ativo { get; private set; }
        public string? Foto { get; private set; }
        public DateTime DtNascimento { get; set; }
        public string Senha { get; set; }

        private readonly UsuarioFixtureTests _fixture;

        public UsuarioBuilder()
        {
            _fixture = new UsuarioFixtureTests();
            var usuario =  _fixture.CriarUsuarioValido();            
            Nome = usuario.Nome;
            Email = usuario.Email.Address;
            Cpf = usuario.Cpf.Numero;
            Ativo = usuario.Ativo;
            Foto = usuario.Foto;
            DtNascimento = usuario.DtNascimento;
            Senha = usuario.Senha.Valor;

        }

        public static UsuarioBuilder Novo()
        {
            return new UsuarioBuilder();
        }        

        public UsuarioBuilder ComNome(string nome)
        {
            Nome = nome;
            return this;
        }

        public UsuarioBuilder ComEmail(string email)
        {
            Email = email;
            return this;
        }

        public UsuarioBuilder ComCpf(string cpf)
        {
            Cpf = cpf;
            return this;
        }

        public UsuarioBuilder ComFoto(string foto)
        {
            Foto = foto;
            return this;
        }

        public UsuarioBuilder ComSenha(string senha)
        {
            Senha = senha;
            return this;
        }

        public UsuarioBuilder ComFlagAtivo(bool ativo)
        {
            Ativo = ativo;
            return this;
        }

        public UsuarioBuilder ComDataNascimento(DateTime data)
        {
            DtNascimento = data;
            return this;
        }

        public Usuario Buid()
        {
            return new Usuario(Nome, Email, Cpf, Senha, Ativo, DtNascimento, Foto);
        }
    }
}
