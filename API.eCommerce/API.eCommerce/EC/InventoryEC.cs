using API.eCommerce.Database;
using Library.eCommerce.DTO;

namespace API.eCommerce.EC
{
    public class InventoryEC
    {
        
        public List<ProductDTO?> Get()
        {
            return FakeDatabase.Inventory;
        }

        public ProductDTO? Delete(int id)
        {
            var itemToDelete = FakeDatabase.Inventory.FirstOrDefault(p => p?.Id == id);
            if(itemToDelete != null)
            {
                FakeDatabase.Inventory.Remove(itemToDelete);
            }
            return itemToDelete;
        }
    }
}
