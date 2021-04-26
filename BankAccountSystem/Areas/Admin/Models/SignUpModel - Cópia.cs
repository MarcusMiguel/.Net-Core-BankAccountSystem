using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountSystem.Models
{
    public class SignUp
    {
        [Required(ErrorMessage = "Por favor digite um email válido."),  EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Por favor digite um nome de usuário válido.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Por favor digite uma senha válida.")]
      //  [DataType(DataType.Password)]
        public string Password { get; set; }
        //[Required(ErrorMessage = "Falha ao confirmar senha.")]
        //public TipoConta TipoConta { get; set; }
        //[Required]
       // public int Saldo { get; set; }
       // [Required]
       // [Range(0, int.MaxValue, ErrorMessage = "O Crédito não pode ser negativo.")]
       // public int Credito { get; set; }
    }
}
