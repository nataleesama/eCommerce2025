
using eCommerce.Models;
using Library.eCommerce.DTO;
using Library.eCommerce.Models;
using Library.eCommerce.Utilities;
using Newtonsoft.Json;

namespace Library.eCommerce.Services
{
    public class CartServiceProxy
    {
        private CartServiceProxy()
        {
            //Cart = new List<ProductInCart?>{ };
            var cartPayload = new WebRequestHandler().Get("/Cart").Result;
            Cart = JsonConvert.DeserializeObject<List<ProductInCart>>(cartPayload) ?? new List<ProductInCart?>();
        }

        public List<ProductInCart?> Cart { get; private set; }
        private static CartServiceProxy? cart;

        public static CartServiceProxy Current
        {
            get
            {
                if (cart == null)
                {
                    cart = new CartServiceProxy();
                }
                return cart;
            }
        }


        public int AddToCart(ProductDTO? product, int amount)
        {
            int add = 1;
            ProductInCart? selectedProduct = Cart.FirstOrDefault(p => p.item.Id == product.Id);
            if (selectedProduct != null)
            {
                if (amount != 1)
                {
                    add = amount - selectedProduct.cartQuantity;
                    selectedProduct.cartQuantity += add;
                }
                else
                {
                    selectedProduct.cartQuantity += amount;
                }
            }
            else
            {
                ProductInCart? newProduct = new ProductInCart(product);
                newProduct.cartQuantity = 1;
                newProduct.inCart = true;
                Cart.Add(newProduct);
            }
            return add;
        }

        public int Delete(ProductDTO product, int totalRemoved)
        {
            int count = 0;
            ProductInCart? selectedProduct = Cart.FirstOrDefault(p => p.item.Id == product.Id);
            if (selectedProduct != null)
            {
                if (selectedProduct.cartQuantity - totalRemoved > 0)
                {
                    count = selectedProduct.cartQuantity - totalRemoved;
                    selectedProduct.cartQuantity = count;
                }
                else
                {
                    selectedProduct.inCart = false;
                    count = selectedProduct.cartQuantity;
                    Cart.Remove(selectedProduct);
                    var result = new WebRequestHandler().Delete($"Cart/{product.Id}").Result;

                    ProductInCart? productNeeded = Cart.FirstOrDefault(p => p.item.Id == product.Id);
                    Cart.Remove(selectedProduct);

                    //return JsonConvert.DeserializeObject<ProductInCart>(result);
                }
            }
            return count;
        }

        public void Drop()
        {
            Cart.Clear();
        }

        public double GetTotal()
        {
            double total = 0;
            foreach (var item in Cart)
            {
                total += item?.item.Price ?? 0 * item?.cartQuantity ?? 0;
            }
            return total;
        }

    };
}
