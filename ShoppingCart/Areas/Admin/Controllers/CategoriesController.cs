using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models;
using ShoppingCart.Models.ViewModels;

namespace ShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly DataContext db;

        public CategoriesController(DataContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 10;
            var pageVM = new PageViewModel(p, db.Categories.Count(), pageSize);

            var indexVM = new IndexViewModel<Category>(
                await db.Categories.Skip((p - 1) * pageSize).Take(pageSize).ToListAsync(),
                pageVM);

            return View(indexVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryViewModel categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }

            Category category = new Category() { Name = categoryVM.Name, Slug = categoryVM.Slug };

            db.Add(category);
            await db.SaveChangesAsync();

            TempData["Success"] = "Категория успешно добавлена!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            return View(new CategoryViewModel(await db.Categories.FindAsync(id)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryViewModel categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }

            Category category = await db.Categories.FindAsync(categoryVM.Id);

            category.Name = categoryVM.Name;
            category.Slug = categoryVM.Slug;

            db.Update(category);
            await db.SaveChangesAsync();

            TempData["Success"] = "Категория успешно изменена!";

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            Category category = await db.Categories.FindAsync(id);

            if (category != null)
            {
                db.Remove(category);
                await db.SaveChangesAsync();

                TempData["Success"] = "Категория успешно удалена!";
            }

            return RedirectToAction("Index");
        }
    }
}