using Hotel.Atr3.RealPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Atr3.RealPortal.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(userLogin.Login);
            if(appUser!=null)
            {
                var result = await _signInManager.PasswordSignInAsync(appUser, userLogin.Password, false, false);
            }

            return View();
        }
    }
}
