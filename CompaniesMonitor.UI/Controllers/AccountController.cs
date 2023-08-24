using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MSGCompaniesMonitor.Models;

namespace CompaniesMonitor.UI.Controllers
{

    [Route("[controller]/[action]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var userObj = new User { UserName = user.UserName, Email = user.Email ,Name = user.Name };
               
                IdentityResult result = await _userManager.CreateAsync(user, user.PasswordHash);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(userObj, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                    
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("Register", error.Description);
                    }
                }
            }

            ViewBag.ShowToast = true;
            ViewBag.ToastMessage = ModelState.Values
                                    .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                                    .ToList();
            ViewBag.ToastType = "error";
            return View(user);


        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(User user)
        {

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, isPersistent: true, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                    
                }
                else
                    ModelState.AddModelError("Login", "Invalid login attempt.");
            }
 
             ViewBag.ShowToast = true;
             ViewBag.ToastMessage = ModelState.Values
                                        .SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                                        .ToList();
             ViewBag.ToastType = "error";
              return View(user);

        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }

}

