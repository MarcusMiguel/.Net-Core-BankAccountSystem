using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BankAccountSystem.Models
{
    public class Transfer
    {
        [Required(ErrorMessage = "Por favor digite um número de conta válido.")]
        public long OtherAccountNumber { get; set; }
        [Required(ErrorMessage = "Por favor digite uma quantia válida.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantia precisa ser maior que 1.")]
        public int Amount { get; set; }
    }
}
