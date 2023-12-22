using AVS.SpotifyMusic.Domain.Contas.Entidades;
using AVS.SpotifyMusic.Domain.Pagamentos.Entidades;
using AVS.SpotifyMusic.Tests.Extensions;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;

namespace AVS.SpotifyMusic.Tests.Fixtures
{
    [CollectionDefinition(nameof(UsuarioCollection))]
    public class UsuarioCollection : ICollectionFixture<UsuarioFixtureTests> { }

    public class UsuarioFixtureTests : IDisposable
    {
        private bool _disposedValue = false;
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
            //var genero = new Faker().PickRandom<Name.Gender>();
            var usuarios = new Faker<Usuario>("pt_BR")
                .CustomInstantiator(f => new Usuario(
                    f.Name.FullName(Name.Gender.Male), 
                    f.Internet.Email(f.Name.FirstName(Name.Gender.Male)).ToLower(),
                    f.Person.Cpf(),
                    @"Abc$123456",//f.Internet.PasswordCustom(8,20),                    
                    ativo,
                    f.Person.DateOfBirth,
                    f.Internet.Avatar()
                    )).Generate(quantidade);
            return usuarios;
        }

        public Usuario CriarUsuarioInvalido()
        {
            var genero = new Faker().PickRandom<Name.Gender>();
            var usuario = new Faker<Usuario>("pt_BR")
                .CustomInstantiator(f => new Usuario(
                    string.Empty,
                    "teste.gmail.com",
                    string.Empty,
                    @"Abc$123456", //f.Internet.PasswordCustom(8,20),                    
                    true,
                    f.Person.DateOfBirth,
                    f.Internet.Avatar()
                    )).Generate();
            return usuario;
        }

        public IEnumerable<Cartao> CriarCartoes(int quantidade, bool ativo)
        {
            var bogus = new Faker<Cartao>("pt_BR");
            bogus.CustomInstantiator(f => new Cartao(
                f.Finance.CreditCardNumber(CardType.Mastercard),
                f.Name.FullName(Name.Gender.Male),
                $"{f.Date.Future().Month:D2}/{f.Date.Future(5).Year}",
                f.Finance.CreditCardCvv(),
                ativo,
                f.Random.Decimal(2800M, 10000M)
                ));
            return bogus.Generate(quantidade);
        }

        public Cartao CriarCartaoValido()
        {
            return CriarCartoes(1, true).First();
        }

        public Task<IEnumerable<Cartao>> ObterCartoes()
        {
            var cartoes = new List<Cartao>();
            cartoes.Add(CriarCartoes(1, false).First());
            cartoes.Add(CriarCartaoValido());
            return Task.FromResult(cartoes.AsEnumerable());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                _disposedValue = true;
            }
        }

        ~UsuarioFixtureTests()
        {
            Dispose(false);
        }
    }
}
