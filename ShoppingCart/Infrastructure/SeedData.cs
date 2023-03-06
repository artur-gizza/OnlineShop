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
                Category fruits = new Category { Name = "Fruits", Slug ="fruits" };
                Category shirts = new Category { Name = "Shirts", Slug = "shirts" };

                context.Products.AddRange(
                    new Product
                    {
                        Name = "Apples",
                        Description = "Juicy red apples",
                        Price = 1.5M,
                        Category = fruits,
                        ImageName = "apples.jpg"
                    },
                    new Product
                    {
                        Name = "Bananas",
                        Description = "Big yellow bananas",
                        Price = 2M,
                        Category = fruits,
                        ImageName = "bananas.jpg"
                    },
                    new Product
                    {
                        Name = "Watermelon",
                        Description = "Round green watermelon",
                        Price = 4M,
                        Category = fruits,
                        ImageName = "watermelon.jpg"
                    },
                    new Product
                    {
                        Name = "Oranges",
                        Description = "Orange oranges",
                        Price = 2.5M,
                        Category = fruits,
                        ImageName = "oranges.jpg"
                    },
                    new Product
                    {
                        Name = "Blue shirt",
                        Description = "Blue shirt",
                        Price = 9.99M,
                        Category = shirts,
                        ImageName = "blue shirt.jpg"
                    },
                    new Product
                    {
                        Name = "Black shirt",
                        Description = "Black shirt",
                        Price = 9.99M,
                        Category = shirts,
                        ImageName = "black shirt.jpg"
                    },
                    new Product
                    {
                        Name = "White shirt",
                        Description = "White shirt",
                        Price = 9.99M,
                        Category = shirts,
                        ImageName = "white shirt.jpg"
                    },
                    new Product
                    {
                        Name = "Red shirt",
                        Description = "Red shirt",
                        Price = 9.99M,
                        Category = shirts,
                        ImageName = "red shirt.jpg"
                    }
                    );

                context.SaveChanges();
            }
        }
    }
}