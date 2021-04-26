using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankAccountSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;

namespace BankAccountSystem.Controllers
{
    [Authorize(Roles="Admin")]
    [Area("Admin")]
    [Route("Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public SignUp User { get; set; }

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Route("Index")]
        public async Task<ViewResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            users.RemoveAt(0);
            return View(users);
        }

        [Route("SignUp")]
        public IActionResult SignUp()
        {
            User = new SignUp();
            return View(User);
        }
        [Route("SignUp")]
        [HttpPost]
        public async Task<ActionResult> SignUp(SignUp model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = User.UserName,
                    Email = User.Email,
                    TipoConta = model.TipoConta,
                    Saldo = model.Saldo,
                    Credito = model.Credito,
                    Bloqueado = false,
                    NumeroConta = DateTime.Now.Ticks
                };
                var result = await _userManager.CreateAsync(user, User.Password);
                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                }
                ModelState.Clear();
                return RedirectToAction("Index");
            }
            throw new Exception("Error message");
        }

        [Route("Block_Unblock")]
        public async Task Block_Unblock(String email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            user.Bloqueado = user.Bloqueado == false ? true : false;
            await _userManager.UpdateAsync(user);
        }
    }
}
