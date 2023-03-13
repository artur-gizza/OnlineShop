using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Это обязательное поле")]
        public string Name { get; set; }

        [Display(Name = "Псевдоним для url")]
        [Required(ErrorMessage = "Это обязательное поле")]
        public string Slug { get; set; }

        public CategoryViewModel()
        {

        }

        public CategoryViewModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Slug = category.Slug;
        }
    }
}

