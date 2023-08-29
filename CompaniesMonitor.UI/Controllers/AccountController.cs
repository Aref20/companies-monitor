using CompaniesMonitor.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace CompaniesMonitor.UI.Controllers
{

    [Route("[controller]/[action]")]
    
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            var roleName = "User"; // Set the desired role name
            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                var role = new IdentityRole(roleName);
                var result = await _roleManager.CreateAsync(role);
            }

                if (ModelState.IsValid)
            {
                var userObj = new User { UserName = user.UserName, Email = user.Email ,Name = user.Name };
               
                IdentityResult result = await _userManager.CreateAsync(user, user.PasswordHash);

                

                if (result.Succeeded )
                {

                    // Select the user, and then add the admin role to the user
                    var user2 = await _userManager.FindByNameAsync(userObj.UserName);
                    if (!await _userManager.IsInRoleAsync(user2, roleName))
                    {
                        var userResult = await _userManager.AddToRoleAsync(user2, roleName);
                    }

                    TempData["ShowToast"] = true;
                    TempData["ToastMessage"] = "User Inserted Successfully";
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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
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

