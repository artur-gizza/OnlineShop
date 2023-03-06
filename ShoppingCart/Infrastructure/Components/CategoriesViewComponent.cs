using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCart.Infrastructure.Components
{
    public class CategoriesViewComponent: ViewComponent
    {
        private readonly DataContext _context;

        public CategoriesViewComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string area = "")
        {
            ViewBag.Area = area;
            return View(await _context.Categories.ToListAsync());
        }
    }
}

