using System.Text.RegularExpressions;

namespace AVS.SpotifyMusic.Domain.Core.ObjValor
{
    public class Email
    {
        public const int EMAIL_TAM_MAXIMO = 254;
        public const int EMAIL_TAM_MINIMO = 5;
        public string Address { get; private set; }

        public Email(string address)
        {
            //if (string.IsNullOrEmpty(address)) throw new ArgumentException("E-mail é obrigatório");
            Address = address;
        }

        public static bool ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return true;
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
            return regexEmail.IsMatch(email);
        }
    }
}
