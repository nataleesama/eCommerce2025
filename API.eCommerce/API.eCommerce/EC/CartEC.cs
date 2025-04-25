using API.eCommerce.Database;
using eCommerce.Models;
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
                itemToDelete.cartQuantity = 0;
                itemToDelete.inCart = false;
            }
            return itemToDelete;
        }

        public int Clear()
        {
            FakeDatabase.Cart.Clear();
            return 1;
        }

        public ProductInCart? AddOrUpdate(ProductInCart prod)
        {

            ProductInCart? selectedProduct = FakeDatabase.Cart.FirstOrDefault(p => p.item.Id == prod.item.Id);
            if (selectedProduct != null)
            {
                selectedProduct.cartQuantity += 1;
            }
            else
            {
                ProductInCart? newProduct = new ProductInCart(prod);
                newProduct.cartQuantity = 1;
                newProduct.inCart = true;
                FakeDatabase.Cart.Add(newProduct);

            }

            return prod;
        }
    }
}
