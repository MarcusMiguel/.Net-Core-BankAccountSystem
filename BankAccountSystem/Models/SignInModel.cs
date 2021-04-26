using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccountSystem.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Por favor digite um email válido."), EmailAddress ]
        public string Email { get; set; }
        [Required(ErrorMessage = "Por favor digite uma senha válida.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Lembre-me")]
        public bool RememberMe { get; set; }
    }
}
