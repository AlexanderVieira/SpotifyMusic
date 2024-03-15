using AVS.SpotifyMusic.Domain.Core.Utils;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AVS.SpotifyMusic.Domain.Core.ObjValor
{
    public class Senha
    {
        //private const string PATTERN = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$";
        private const string PATTERN = @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#!$%]).{8,20})";
        public string Valor { get; private set; }

        public Senha(string valor)
        {
            Validation.ValidarSeNuloVazio(valor, "Ocorreu um erro genérico.");
            var isMatch = ValidarFormato(valor); 
            if (isMatch)
            {
                Valor = CriptografarSenha(valor);
            }
        }

        public static string CriptografarSenha(string senhaTextoPlano)
        {
            if (string.IsNullOrEmpty(senhaTextoPlano)) return string.Empty;
            var sha = SHA256.Create();
            var bTexto = Encoding.UTF8.GetBytes(senhaTextoPlano);
            var cripto = sha.ComputeHash(bTexto);
            return Convert.ToHexString(cripto);
        }

        public static bool ValidarFormato(string valor) 
        {
            if (valor == null) { return false; }
            return Regex.IsMatch(valor, PATTERN);
        }
    }
}
