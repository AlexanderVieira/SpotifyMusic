using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AVS.SpotifyMusic.Api.Models
{
    public class UserRegister
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string UltimoNome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        //public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }
        // public string BirthDate { get; set; }
        //public string Foto { get; set; }
        // public string GenderType { get; set; }
        // public string FunctionType { get; set; }
        //public bool Ativo { get; set; }
        //public string NumTelefone { get; set; }
        // public string PhoneType { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string ConfirmePassword { get; set; }
        
    }
}