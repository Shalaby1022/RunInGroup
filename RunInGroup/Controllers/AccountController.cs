using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RunInGroup.Data;
using RunInGroup.Models;
using RunInGroup.ViewModel;
using static System.Reflection.Metadata.BlobBuilder;

namespace RunInGroup.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RunDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RunDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]

        public async Task<IActionResult> login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.Email);
            if (user != null)
            {
                var checkpassword = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (checkpassword)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                }
                TempData["Erro"] = "Wrong credentials! please try again";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentials! please try again";
            return View(loginVM);
        }


    }
}

