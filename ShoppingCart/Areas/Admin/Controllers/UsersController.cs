using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Models;
using ShoppingCart.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        public UserManager<IdentityUser> _userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                IdentityUser newUser = new IdentityUser { UserName = user.UserName, Email = user.Email };
                IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);

                if (result.Succeeded)
                {
                    TempData["Success"] = "The user has been created!";

                    return RedirectToAction("Index");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);

            UserViewModel userEdit = new UserViewModel(user);

            return View(userEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = await _userManager.FindByIdAsync(user.Id);
                identityUser.UserName = user.UserName;
                identityUser.Email = user.Email;

                IdentityResult result = await _userManager.UpdateAsync(identityUser);

                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(user.Password))
                    {
                        await _userManager.RemovePasswordAsync(identityUser);
                        result = await _userManager.AddPasswordAsync(identityUser, user.Password);
                    }

                    TempData["Success"] = "The user has been edited!";

                    return RedirectToAction("Index");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }

            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);

            if(user != null)
            {
                await _userManager.DeleteAsync(user);

                TempData["Success"] = "The user has been deleted!";
            }

            return RedirectToAction("Index");
        }
    }
}

