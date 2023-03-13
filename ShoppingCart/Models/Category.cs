using System;
using System.ComponentModel.DataAnnotations;
using ShoppingCart.Models.ViewModels;

namespace ShoppingCart.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Это обязательный атрибут")]
        public string Name { get; set; }

        [Display(Name = "Псевдоним на английском для адресной строки")]
        [Required(ErrorMessage = "Это обязательный атрибут")]
        public string Slug { get; set; }
    }
}

