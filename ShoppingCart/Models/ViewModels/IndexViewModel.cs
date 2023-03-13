using System;
namespace ShoppingCart.Models.ViewModels
{
    public class IndexViewModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public PageViewModel PageVM { get; set; }

        public IndexViewModel(IEnumerable<T> items, PageViewModel pageVM)
        {
            Items = items;
            PageVM = pageVM;
        }
    }
}

