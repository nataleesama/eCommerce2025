
using eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class CartServiceProxy
    {
        private CartServiceProxy()
        {
            Cart = new Dictionary<Product, int>();
        }

        public Dictionary<Product, int> Cart { get; private set; }
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


        public int AddToCart(Product? product, int amount)
        {
            int add = 1;
            if (product != null && Cart.ContainsKey(product))
            {
                if (amount != 1)
                {
                    add = amount - Cart[product];
                    Cart[product] += add;
                }
                else
                {
                    Cart[product] += amount;
                }
            }
            else
            {
                Cart.Add(product, amount);
            }
            return add;
        }

        public int Delete(Product product, int total)
        {
            int count = 0;
            if (Cart.ContainsKey(product))
            {
                if (total > 0)
                {
                    count = Cart[product] - total;
                    Cart[product] = total;
                }
                else
                {
                    count = Cart[product];
                    Cart.Remove(product);
                }
            }
            return count;
        }

        public void Drop()
        {
            Cart.Clear();
        }

    };
}
