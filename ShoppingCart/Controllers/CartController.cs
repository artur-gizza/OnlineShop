using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Infrastructure;
using ShoppingCart.Models;
using ShoppingCart.Models.ViewModels;

namespace ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext db;

        public CartController(DataContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Total)
            };

            return View(cartVM);
        }

        public async Task<IActionResult> Add(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return View("Error", new ErrorViewModel() { Message = "This product is not exists" });
            }

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(item => item.ProductId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
            }
            else
            {
                cartItem.Quanity++;
            }

            HttpContext.Session.SetJson("Cart", cart);

            TempData["Success"] = "The product has been added!";

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            if (cart == null)
            {
                return View("Error", new ErrorViewModel() { Message = "Cart is empty" });
            }

            CartItem cartItem = cart.Where(item => item.ProductId == id).FirstOrDefault();
            if (cartItem == null)
            {
                return View("Error", new ErrorViewModel() { Message = "There is no such product in cart" });
            }

            if (cartItem.Quanity > 1)
            {
                cartItem.Quanity--;
                HttpContext.Session.SetJson("Cart", cart);
            }
            else
            {
                cart.RemoveAll(item => item.ProductId == id);
                if (cart.Count == 0)
                {
                    HttpContext.Session.Remove("Cart");
                }
                else
                {
                    HttpContext.Session.SetJson("Cart", cart);
                }
            }

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            if (cart == null)
            {
                return View("Error", new ErrorViewModel() { Message = "Cart is empty" });
            }

            cart.RemoveAll(item => item.ProductId == id);
            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "The product has been removed!";

            return RedirectToAction("Index");
        }
    }
}

