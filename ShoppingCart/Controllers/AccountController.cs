using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Models;
using ShoppingCart.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, false, false);

                if (result.Succeeded)
                {
                    return Redirect(loginVM.ReturnUrl ?? "/");
                }

                ModelState.AddModelError("", "Invalid username or password");
            }

            return View(loginVM);
        }

        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                if (registerVM.Password != registerVM.RepeatPassword)
                {
                    ModelState.AddModelError("", "Введенные пароли отличаются");
                    return View(registerVM);
                }

                IdentityUser newUser = new IdentityUser { UserName = registerVM.UserName, Email = registerVM.Email };
                IdentityResult result = await _userManager.CreateAsync(newUser, registerVM.Password);

                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(registerVM.UserName, registerVM.Password, false, false);
                    return Redirect(registerVM.ReturnUrl ?? "/");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(registerVM);
        }

        public async Task<IActionResult> Details()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                return View(new DetailsViewModel { UserName = user.UserName, Email = user.Email });
            }

            return RedirectToAction("Login");
        }

        public async Task<RedirectResult> Logout(string returnUrl)
        {
            await _signInManager.SignOutAsync();

            return Redirect(returnUrl ?? "/");
        }
    }
}
