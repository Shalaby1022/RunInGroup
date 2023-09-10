using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RunInGroup.Data;
using RunInGroup.Models;
using RunInGroup.ViewModel;
using System.Security.Cryptography.X509Certificates;
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
                TempData["Error"] = "Wrong Password credentials! please try again";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong username credentials! please try again";
            return View(loginVM);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var reponse = new RegisterViewModel();
            return View(reponse);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }

            var user = await _userManager.FindByEmailAsync(registerVM.Email);
            if(user != null)
            {
                TempData["Error"] = "This Email Address Is Already In Use";
            }

            var Newuser = new AppUser
            {
                UserName = registerVM.Email,
                Email = registerVM.Email,
                // Other user properties
            };
            var newusercreate = await _userManager.CreateAsync(Newuser, registerVM.Password);
            try
            {
                if (newusercreate.Succeeded)
                {
                    await _userManager.AddToRoleAsync(Newuser, UserRoles.User);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception here
                TempData["Error"] = "An error occurred while assigning the role.";
                // You might also want to return an error view or redirect to an error page.
            }

            return RedirectToAction("Index", "Home");

            
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return View("Index" , "Race");
        }


    }
}

