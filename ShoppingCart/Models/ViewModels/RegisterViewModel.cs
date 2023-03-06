using System;
using System.ComponentModel.DataAnnotations;

namespace ShoppingCart.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Display(Name = "Почта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [DataType(DataType.Password), Required]
        public string Password { get; set; }

        [Display(Name = "Повторите пароль")]
        [DataType(DataType.Password), Required(ErrorMessage = "Введите пароль повторно")]
        public string RepeatPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}

