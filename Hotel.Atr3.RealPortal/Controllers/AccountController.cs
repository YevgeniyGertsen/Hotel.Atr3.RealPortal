using Hotel.Atr3.RealPortal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Atr3.RealPortal.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private TokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;

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
                if (result.Succeeded)
                {
                    var token = await _tokenService.GenerateAccessToken(appUser);
                    Response.Cookies.Append("token", token);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }
    }
}
