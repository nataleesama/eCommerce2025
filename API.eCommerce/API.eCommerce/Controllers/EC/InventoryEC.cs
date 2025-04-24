using Library.eCommerce.DTO;

namespace API.eCommerce.Controllers.EC
{
    public class InventoryEC
    {
        public List<ProductDTO?> Get()
        {
            return new List<ProductDTO?>
            {
                new ProductDTO{Id=1, Name = "Product 1 WebTest", Price = 15.99, Quantity = 5 },
                new ProductDTO{Id=2, Name = "Product 2", Price = 12.99, Quantity = 3 },
                new ProductDTO{Id=3, Name = "Product 3", Price = 20.99, Quantity = 2 }
            };
        }
    }
}
