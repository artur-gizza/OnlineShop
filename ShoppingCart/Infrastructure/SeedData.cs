using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Infrastructure
{
    public class SeedData
    {
        public static void SeedDataBase(DataContext context)
        {
            context.Database.Migrate();

            if (!context.Products.Any())
            {
                Category fruits = new Category { Name = "Фрукты", Slug ="fruits" };
                Category shirts = new Category { Name = "Футболки", Slug = "shirts" };

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Яблоки",
                        Price = 50,
                        Category = fruits,
                        ImageName = "apples.jpg"
                    },
                    new Product
                    {
                        Name = "Бананы",
                        Price = 70,
                        Category = fruits,
                        ImageName = "bananas.jpg"
                    },
                    new Product
                    {
                        Name = "Арбуз",
                        Price = 200,
                        Category = fruits,
                        ImageName = "watermelon.jpg"
                    },
                    new Product
                    {
                        Name = "Апельсины",
                        Price = 50,
                        Category = fruits,
                        ImageName = "oranges.jpg"
                    },
                    new Product
                    {
                        Name = "Синяя футболка",
                        Price = 600,
                        Category = shirts,
                        ImageName = "blue shirt.jpg"
                    },
                    new Product
                    {
                        Name = "Черная футболка",
                        Price = 600,
                        Category = shirts,
                        ImageName = "black shirt.jpg"
                    },
                    new Product
                    {
                        Name = "Белая футболка",
                        Price = 700,
                        Category = shirts,
                        ImageName = "white shirt.jpg"
                    },
                    new Product
                    {
                        Name = "Красная футболка",
                        Price = 1000,
                        Category = shirts,
                        ImageName = "red shirt.jpg"
                    }
                    );

                context.SaveChanges();
            }
        }
    }
}