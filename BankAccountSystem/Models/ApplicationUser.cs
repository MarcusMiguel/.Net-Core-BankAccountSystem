using Microsoft.AspNetCore.Identity;

namespace BankAccountSystem.Models
{
    public class ApplicationUser : IdentityUser
    {
        public long NumeroConta { get; set; }
        public TipoConta TipoConta { get; set; }
        public int Saldo { get; set; }
        public int Credito { get; set; }
        public bool Bloqueado { get; set; } = false;
    }
}
