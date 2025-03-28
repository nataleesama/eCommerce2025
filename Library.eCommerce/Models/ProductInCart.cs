using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Library.eCommerce.Models
{
    public class ProductInCart
    {
        public Product item { get; set; }
        public int cartQuantity { get; set; }

        public string? Display
        {
            get
            {
                return $"{cartQuantity}x {item.Name} - ${item.Price * cartQuantity}";
            }
        }

        public ProductInCart()
        {
            item = new Product();
            cartQuantity = 0;
        }

        // Copy Constructor
        public ProductInCart(ProductInCart p)
        {
            item = p.item;
            cartQuantity = p.cartQuantity;
        }
        public ProductInCart(Product p)
        {
            item = p;
            cartQuantity = 0;
        }

        public override string ToString()
        {
            return Display ?? string.Empty;
        }
    }
}
