using System;
namespace ShoppingCart.Models
{
    public class CartItem
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public int Quanity { get; set; }
        public decimal Total
        {
            get { return Quanity * ProductPrice; }
        }

        public CartItem()
        {

        }

        public CartItem(Product product)
        {
            ProductId = product.Id;
            ProductName = product.Name;
            ProductPrice = product.Price;
            ProductImage = product.ImageName;
            Quanity = 1;
        }
    }
}

