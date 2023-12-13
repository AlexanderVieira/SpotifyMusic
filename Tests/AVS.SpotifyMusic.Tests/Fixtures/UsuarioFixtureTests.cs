using AVS.SpotifyMusic.Domain.Conta.Entidades;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;

namespace AVS.SpotifyMusic.Tests.Fixtures
{
    [CollectionDefinition(nameof(UsuarioCollection))]
    public class UsuarioCollection : ICollectionFixture<UsuarioFixtureTests> { }

    public class UsuarioFixtureTests : IDisposable
    {
         public Usuario CriarUsuarioValido()
        {
            return CriarUsuarios(1, true).First();
        }

        public Task<IEnumerable<Usuario>> ObterUsuarios()
        {
            var usuarios = new List<Usuario>();
            usuarios.AddRange(CriarUsuarios(50, true).ToList());
            usuarios.AddRange(CriarUsuarios(50, false).ToList());
            return Task.FromResult(usuarios.AsEnumerable());
        }

        private IEnumerable<Usuario> CriarUsuarios(int quantidade, bool ativo)
        {
            var genero = new Faker().PickRandom<Name.Gender>();
            var usuarios = new Faker<Usuario>("pt_BR")
                .CustomInstantiator(f => new Usuario(
                    f.Name.FullName(genero), 
                    f.Internet.Email(),
                    f.Person.Cpf(),
                    f.Internet.Password(),
                    f.Internet.Avatar(),
                    ativo,
                    f.Person.DateOfBirth
                    )).Generate(quantidade);
            return usuarios;
        }

        public Usuario CriarUsuarioInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();
            var usuario = new Faker<Usuario>("pt_BR")
                .CustomInstantiator(f => new Usuario(
                    string.Empty,
                    "teste",
                    string.Empty,
                    f.Internet.Password(),
                    f.Internet.Avatar(),
                    true,
                    f.Person.DateOfBirth
                    )).Generate();
            return usuario;
        }

        public void Dispose()
        {            
        }
    }
}
