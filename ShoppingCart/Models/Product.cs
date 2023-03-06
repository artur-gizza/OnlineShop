using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShoppingCart.Infrastructure.Validation;

namespace ShoppingCart.Models
{
    public class Product
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        public string Name { get; set; }

        [Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public string ImageName { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "You must choose a category")]
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }


public static class CategoryEndpoints
{
	public static void MapCategoryEndpoints (this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Category", () =>
        {
            return new [] { new Category() };
        })
        .WithName("GetAllCategorys");

        routes.MapGet("/api/Category/{id}", (int id) =>
        {
            //return new Category { ID = id };
        })
        .WithName("GetCategoryById");

        routes.MapPut("/api/Category/{id}", (int id, Category input) =>
        {
            return Results.NoContent();
        })
        .WithName("UpdateCategory");

        routes.MapPost("/api/Category/", (Category model) =>
        {
            //return Results.Created($"/Categorys/{model.ID}", model);
        })
        .WithName("CreateCategory");

        routes.MapDelete("/api/Category/{id}", (int id) =>
        {
            //return Results.Ok(new Category { ID = id });
        })
        .WithName("DeleteCategory");  
    }
}}

