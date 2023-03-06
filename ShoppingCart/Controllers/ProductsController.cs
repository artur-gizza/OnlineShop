using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models;

namespace ShoppingCart.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext db;

        public ProductsController(DataContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(string categorySlug = "", int p = 1)
        {
            int pageSize = 3;
            ViewBag.CurrentPage = p;
            ViewBag.CategorySlug = categorySlug;

            if (p < 1) p = 1;

            if(categorySlug == "")
            {
                ViewBag.TotalPages = (int)Math.Ceiling((decimal)db.Products.Count() / pageSize);

                return View(await db.Products.Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
            }

            Category category = await db.Categories.Where(c => c.Slug == categorySlug).FirstOrDefaultAsync();

            if (category == null) return Redirect("/Products");

            var productsByCategory = db.Products.Where(p => p.CategoryId == category.Id);

            ViewBag.TotalPages = (int)Math.Ceiling((decimal)productsByCategory.Count() / pageSize);

            return View(await productsByCategory.Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
        }
    }
}

