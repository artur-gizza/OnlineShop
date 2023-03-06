using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ShoppingCart.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password), Required, MinLength(6, ErrorMessage = "Minimum length is 6")]
        public string Password { get; set; }

        public UserViewModel()
        {
        }

        public UserViewModel(IdentityUser user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Email = user.Email;
            Password = user.PasswordHash;
        }
    }
}

