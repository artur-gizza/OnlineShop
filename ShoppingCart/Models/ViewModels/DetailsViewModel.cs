using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

namespace ShoppingCart.Models.ViewModels
{
    public class DetailsViewModel
    {
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Display(Name = "Почта")]
        public string Email { get; set; }
    }
}

