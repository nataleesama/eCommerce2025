using API.eCommerce.Database;
using eCommerce.Models;
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

        public ProductDTO? AddOrUpdate(ProductDTO item)
        {
            if(item.Id == 0)
            {
                item.Id = FakeDatabase.LastKey_Item + 1;
            }
            else
            {
                var existingItem = FakeDatabase.Inventory.FirstOrDefault(p => p.Id == item.Id);
                var index = FakeDatabase.Inventory.IndexOf(existingItem);
                FakeDatabase.Inventory.RemoveAt(index);
                FakeDatabase.Inventory.Insert(index, new ProductDTO(item)); 
            }
            FakeDatabase.Inventory.Add(item);

            return item;
        }
    }
}
