using eCommerce.Models;
using Library.eCommerce.Models;
using Library.eCommerce.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class ShopViewModel : INotifyPropertyChanged
    {
        public Product? SelectedProduct { get; set; }
        public ProductInCart? SelectedProdCart { get; set; }
        private ProductServiceProxy _svc = ProductServiceProxy.Current;
        private CartServiceProxy _cartsvc = CartServiceProxy.Current;

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshLists()
        {
            NotifyPropertyChanged(nameof(Products));
            NotifyPropertyChanged(nameof(Cart));
        }
        public ObservableCollection<Product?> Products
        {
            get
            {
                return new ObservableCollection<Product?>(_svc.Products);
            }
        }

        public ObservableCollection<ProductInCart?> Cart
        {
            get
            {
                return new ObservableCollection<ProductInCart?>(_cartsvc.Cart);
            }
        }

        public int AddToCart()
        {
            if(SelectedProduct != null && SelectedProduct.Quantity != 0)
            {
                var id = _cartsvc.AddToCart(SelectedProduct ?? null, 1);
                SelectedProduct.Quantity -= 1;
                var item = _svc.AddOrUpdate(SelectedProduct);
                RefreshLists();
                return id;
            }

            //NotifyPropertyChanged("Cart");
            return -1;
        }

        public int RemoveFromCart()
        {
            if (SelectedProdCart != null && SelectedProdCart.inCart == true)
            {
                var id = _cartsvc.Delete(SelectedProdCart.item, 1);
                SelectedProdCart.item.Quantity += 1;
                var item = _svc.AddOrUpdate(SelectedProdCart.item);
                RefreshLists();
                return id;
            }
            return -1;
        }
    }
}
