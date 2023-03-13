using System;
using System.Drawing.Printing;

namespace ShoppingCart.Models.ViewModels
{
    public class PageViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string UrlSlug { get; set; }

        public PageViewModel(int currentPage, int countOfObjects, int pageSize, string urlSlug = "")
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling((double)countOfObjects / pageSize);
            UrlSlug = urlSlug;
        }
    }
}

