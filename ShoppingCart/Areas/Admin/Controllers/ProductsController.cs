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

namespace ShoppingCart.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly DataContext db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            db = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(string categorySlug = "", int p = 1)
        {
            int pageSize = 10;
            ViewBag.CurrentPage = p;
            ViewBag.CategorySlug = categorySlug;

            if (categorySlug == "")
            {
                ViewBag.TotalPages = (int)Math.Ceiling((decimal)db.Products.Count() / pageSize);

                return View(await db.Products.Include(p => p.Category)
                .Skip((p - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync());
            }

            Category category = await db.Categories.Where(c => c.Slug == categorySlug).FirstOrDefaultAsync();
            if (category == null) return RedirectToAction("Index");

            var productsByCategory = db.Products.Where(p => p.CategoryId == category.Id);

            ViewBag.TotalPages = (int)Math.Ceiling((decimal)productsByCategory.Count() / pageSize);

            return View(await productsByCategory.Skip((p - 1) * pageSize).Take(pageSize).ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name", product.CategoryId);

            if (ModelState.IsValid)
            {
                if (product.ImageUpload != null)
                {
                    product.ImageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;

                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "media/products", product.ImageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                }

                db.Add(product);
                await db.SaveChangesAsync();

                TempData["Success"] = "The product has been created!";
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public async Task<IActionResult> Update(long id)
        {
            Product product = await db.Products.FindAsync(id);

            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name", product.CategoryId);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(long id, Product product)
        {
            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name", product.CategoryId);

            if (ModelState.IsValid)
            {
                if (product.ImageUpload != null)
                {
                    string imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");

                    string oldImagePath = Path.Combine(imageDirectory, product.ImageName);

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }

                    product.ImageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;

                    string newImagePath = Path.Combine(_webHostEnvironment.WebRootPath, "media/products", product.ImageName);

                    FileStream fs = new FileStream(newImagePath, FileMode.Create);
                    await product.ImageUpload.CopyToAsync(fs);
                    fs.Close();
                }

                db.Update(product);
                await db.SaveChangesAsync();

                TempData["Success"] = "The product has been edited!";
            }

            return View(product);
        }


        public async Task<IActionResult> Delete(long id)
        {
            Product product = await db.Products.FindAsync(id);

            if(product == null)
            {
                return RedirectToAction("Index");
            }

            if(product.ImageName != null)
            {
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "media/products", product.ImageName);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            db.Remove(product);
            await db.SaveChangesAsync();

            TempData["Success"] = "The product has been deleted!";

            return RedirectToAction("Index");
        }
    }
}