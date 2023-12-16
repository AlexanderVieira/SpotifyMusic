using AVS.SpotifyMusic.Domain.Core.ObjValor;

namespace AVS.SpotifyMusic.Domain.Core.Utils
{
    public static class ValidationUtils
    {
        public static bool HasValidEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                return Email.ValidarEmail(email);
            }
            else
            {
                return true;
            }
        }

        public static bool HasValidCpf(string strCpf)
        {
            if (!string.IsNullOrWhiteSpace(strCpf))
            {
                return Cpf.ValidarCpf(strCpf);
            }
            else
            {
                return true;
            }
        }

        public static bool HasValidBirthDate(DateTime? date)
        {
            
            if (date != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static Email? ValidateRequestEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                return new Email(email);
            }
            else
            {
                return null;
            }
        }

        public static Cpf? ValidateRequestCpf(string cpf)
        {
            if (!string.IsNullOrWhiteSpace(cpf))
            {
                return new Cpf(cpf);
            }
            else
            {
                return null;
            }
        }

        public static bool ValidarSeGuidIsEmpty(Guid id)
        {
            if (id == Guid.Empty) return true;
            return false;
        }
        
    }
}
