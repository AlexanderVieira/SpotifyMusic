namespace AVS.SpotifyMusic.Domain.Streaming.ObjValor
{
    public record class Duracao
    {
        public int Valor { get; set; }

        public Duracao(int valor)
        {
            if (valor < 0)
            {
                throw new ArgumentException("Duracao da musica nao pode ser negativa");                
            }

            Valor = valor;
        }

        public string Formatar()
        {
            int minutos = Valor * 60;
            int segundos = Valor % 60;
            return $"{minutos.ToString().PadLeft(1, '0')}:{segundos.ToString().PadLeft(1, '0')}";
        }

        public static implicit operator int(Duracao d) => d.Valor;
        public static implicit operator Duracao(int valor) => new Duracao(valor);

        // Exemplo usando class
        //public static bool operator ==(Duracao d1, Duracao d2) => d2.Valor == d1.Valor;
        //public static bool operator !=(Duracao d1, Duracao d2) => d2.Valor == d1.Valor;
        //public static bool operator ==(Duracao d1, int d2) => d2 == d1.Valor;
        //public static bool operator !=(Duracao d1, int d2) => d2 == d1.Valor;
    }
}
