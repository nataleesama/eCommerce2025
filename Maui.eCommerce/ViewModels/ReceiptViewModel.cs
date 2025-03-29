using Library.eCommerce.Models;
using Library.eCommerce.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class ReceiptViewModel
    {
        private CartServiceProxy _cartsvc = CartServiceProxy.Current;

        public ObservableCollection<ProductInCart?> Cart
        {
            get
            {
                return new ObservableCollection<ProductInCart?>(_cartsvc.Cart);
            }
        }

        public void DropCart()
        {
            _cartsvc.Drop();
        }
    }
}
