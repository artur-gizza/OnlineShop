using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models;
using ShoppingCart.Models.ViewModels;

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
            IQueryable<Product> productsByCategory;

            if (p < 1) p = 1;

            //можно завернуть в функцию когда буду выносить логику
            if (categorySlug == "")
            {
                productsByCategory = db.Products;
            }
            else
            {
                Category category = await db.Categories.Where(c => c.Slug == categorySlug).FirstOrDefaultAsync();
                if (category == null) return RedirectToAction("Index");

                productsByCategory = db.Products.Where(p => p.CategoryId == category.Id);
            }

            PageViewModel pageVM = new PageViewModel(p, productsByCategory.Count(), pageSize, categorySlug);
            var indexVM = new IndexViewModel<Product>(
                await productsByCategory.Skip((p - 1) * pageSize).Take(pageSize).ToListAsync(),
                pageVM
                );

            return View(indexVM);
        }
    }
}

