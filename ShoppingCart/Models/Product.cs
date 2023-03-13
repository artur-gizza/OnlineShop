using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ShoppingCart.Infrastructure.Validation;

namespace ShoppingCart.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Please enter a value")]
        public int Price { get; set; }

        [Display(Name = "Image")]
        public string ImageName { get; set; }

        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }

        [Required, Range(1, int.MaxValue, ErrorMessage = "You must choose a category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

