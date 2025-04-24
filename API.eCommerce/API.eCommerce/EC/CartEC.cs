using API.eCommerce.Database;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;

namespace API.eCommerce.EC
{
    public class CartEC
    {
        public List<ProductInCart?> Get()
        {
            return FakeDatabase.Cart;
        }

        public ProductInCart? Delete(int id)
        {
            var itemToDelete = FakeDatabase.Cart.FirstOrDefault(p => p?.item.Id == id);
            if (itemToDelete != null)
            {
                FakeDatabase.Cart.Remove(itemToDelete);
            }
            return itemToDelete;
        }
    }
}
