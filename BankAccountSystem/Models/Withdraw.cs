using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccountSystem.Models
{
    public class Withdraw
    {
        [Required(ErrorMessage = "Por favor digite uma quantia válida.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantia precisa ser maior ou igual a 1.")]
        public int Amount { get; set; }
    }
}
