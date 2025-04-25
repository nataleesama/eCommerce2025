using Library.eCommerce.DTO;
using Library.eCommerce.Models;

namespace API.eCommerce.Database
{
    public static class FakeDatabase
    {
        public static int LastKey_Item
        {
            get
            {
                if (!inventory.Any())
                {
                    return 0;
                }
                return inventory.Select(p => p?.Id ?? 0).Max();
            }
        }

        private static List<ProductDTO?> inventory = new List<ProductDTO?>
        {
            new ProductDTO{Id=1, Name = "Product 1 WebTest", Price = 15.99, Quantity = 5 },
            new ProductDTO{Id=2, Name = "Product 2", Price = 12.99, Quantity = 3 },
            new ProductDTO { Id = 3, Name = "Product 3", Price = 20.99, Quantity = 2 }
        };
        public static List<ProductDTO?> Inventory
        {
            get
            {
                return inventory; 
            }
        }

        private static List<ProductInCart?> cart = new List<ProductInCart?>{};
        public static List<ProductInCart?> Cart
        {
            get
            {
                return cart;
            }
        }
    }
}
