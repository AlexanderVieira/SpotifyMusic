using AVS.SpotifyMusic.Domain.Transacao.Entidades;
using Bogus;
using Bogus.DataSets;

namespace AVS.SpotifyMusic.Tests.Fixtures
{
    [CollectionDefinition(nameof(CartaoCollection))]
    public class CartaoCollection : ICollectionFixture<CartaoFixtureTests> { }

    public class CartaoFixtureTests : IDisposable
    {
        private bool _disposedValue = false;

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

        public Cartao CriarCartaoInvalido()
        {
            var bogus = new Faker<Cartao>("pt_BR");
            bogus.CustomInstantiator(f => new Cartao(
                string.Empty,
                string.Empty,
                $"{f.Date.Future().Month:D2}/{f.Date.Future(5).Year}",
                f.Finance.CreditCardCvv(),
                true,
                f.Random.Decimal(2800M, 10000M)
                ));
            return bogus.Generate();
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

        ~CartaoFixtureTests()
        {
            Dispose(false);
        }
    }
}
