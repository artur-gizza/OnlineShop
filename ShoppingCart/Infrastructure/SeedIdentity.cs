using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Infrastructure
{
    public class SeedIdentity
    {
        public static async void SeedIdentityDataBase(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            if (!userManager.Users.Any())
            {
                await userManager.CreateAsync(
                    new IdentityUser { UserName = "Admin", Email = "admin@mail.ru" },
                    "123456"
                    );

                await roleManager.CreateAsync(new IdentityRole("Admin"));

                var result = await userManager.AddToRoleAsync(await userManager.FindByNameAsync("Admin"), "Admin");
            }
        }
    }
}