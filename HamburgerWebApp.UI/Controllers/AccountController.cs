using HamburgerWebApp.Entity.Concrete;
using HamburgerWebApp.UI.Models.UserVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HamburgerWebApp.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //register action result
        public IActionResult Register()
        {
            return View();
        }
        //register post action result
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    UserName = registerModel.Email.Split('@')[0],
                    Email = registerModel.Email
                };
                var result = _userManager.CreateAsync(user, registerModel.Password).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Account");
                }

                ModelState.AddModelError("", "Something went wrong.");
            }
            return View(registerModel);
        }

        public IActionResult Login()
        {
            return View();
        }
        //login post action result
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginModel.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "There is no user with this email address.");
                    return View(loginModel);
                }
                var result = _signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.RememberMe, false).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Something went wrong.");
            }
            return View(loginModel);
        }
        //logout action result
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
