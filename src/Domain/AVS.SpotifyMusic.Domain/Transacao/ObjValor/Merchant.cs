namespace AVS.SpotifyMusic.Domain.Transacao.ObjValor
{
    public record class Merchant
    {
        public string Nome { get; private set; }

        public Merchant(string nome)
        {
            Nome = nome;
        }
    }
}
