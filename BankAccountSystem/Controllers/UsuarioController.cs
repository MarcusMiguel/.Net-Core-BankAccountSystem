using BankAccountSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System;

namespace BankAccountSystem.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsuarioController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
     
        public IActionResult Index()
        {
            return RedirectToAction("SignIn");
        }

        [Route("SignIn")]
        public IActionResult SignIn()
        {
            return View();
        }

        [Route("SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if ( user.Bloqueado == true)
                {
                    ModelState.AddModelError("", "Usuário bloqueado.");
                    return View(model);
                }
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, true);
                    if (result.Succeeded)
                    {
                        if (model.Email.Equals("admin@admin"))
                        {
                            return RedirectToAction("SignInAdmin");
                        }
                        return RedirectToAction("UserPage");
                    }
                    if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError("", "Login não permitido.");
                    }
                    else if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "Conta bloqueada, tente denovo mais tarde.");
                    }
                }
                ModelState.AddModelError("", "Credenciais inválidas.");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult SignInAdmin(SignInModel model)
        {
            return RedirectToAction("Index", "Home", new { Area="Admin"});
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Usuario");
        }


        [Route("UserPage")]
        public async Task<IActionResult> UserPage()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return View(user);
        }

        [Route("Transfer")]
        public  ViewResult Transfer()
        {
            var transfer = new Transfer();
            return View(transfer);
        }

        [Route("Transfer")]
        [HttpPost]
        public async Task<IActionResult> Transfer(Transfer model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var users = _userManager.Users;
                ApplicationUser otheruser = null;  
                foreach(var u in users)
                {
                    if (u.NumeroConta == Convert.ToInt64(model.OtherAccountNumber))
                    {
                        otheruser = u;
                    }
                }
                if (otheruser == null)
                {
                    ModelState.AddModelError("", "Email não encontrado.");
                    throw new Exception("Error message");
                }
                if (otheruser.Email == user.Email)
                {
                    ModelState.AddModelError("", "O email deve ser diferente do seu.");
                    return View(model);
                }
                if (otheruser.Bloqueado == true)
                {
                    ModelState.AddModelError("", "Conta bloqueada.");
                    return View(model);
                }
                if (user.Saldo + user.Credito >= model.Amount)
                {
                    otheruser.Saldo += model.Amount;
                    user.Saldo -= model.Amount;
                    await _userManager.UpdateAsync(otheruser);
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("UserPage");
                }
                ModelState.AddModelError("", "Crédito insuficiente.");
            }
            throw new Exception("Error message");
        }

        [Route("Deposit")]
        public ViewResult Deposit()
        {
            var deposit = new Deposit();
            return View(deposit);
        }

        [Route("Deposit")]
        [HttpPost]
        public async Task<IActionResult> Deposit(Deposit model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                user.Saldo += model.Amount;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("UserPage");
            }
            return View(model);
        }

        [Route("Withdraw")]
        public ViewResult Withdraw()
        {
            var withdraw = new Withdraw();
            return View(withdraw);
        }

        [Route("Withdraw")]
        [HttpPost]
        public async Task<IActionResult> Withdraw(Withdraw model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);

                if (model.Amount < user.Saldo + user.Credito)
                {
                    user.Saldo -= model.Amount;
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("UserPage");
                }
                else 
                {
                    ModelState.AddModelError("", "Limite de crédito atingido.");
                }
            }
            return View(model);
        }
    }
}
