using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankAccountSystem.Models
{
    public class Deposit
    {
        [Required(ErrorMessage = "Por favor digite uma quantia válida.")]
        [Range(1, 10000, ErrorMessage = "A quantia precisa ser maior que 1 e menor que 10000.")]
        public int Amount { get; set; }
    }
}
