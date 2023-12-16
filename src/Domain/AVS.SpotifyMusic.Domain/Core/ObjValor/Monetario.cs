namespace AVS.SpotifyMusic.Domain.Core.ObjValor
{
    public record class Monetario
    {
        public decimal Valor { get; set; }        

        public Monetario(decimal valor)
        {            
            Valor = valor;
        }

        public string Formatar()
        {
            return $"R$ {Valor.ToString("N2")}";
        }

        public static implicit operator decimal(Monetario d) => d.Valor;

        public static implicit operator Monetario(decimal valor) => new Monetario(valor);
    }
}
