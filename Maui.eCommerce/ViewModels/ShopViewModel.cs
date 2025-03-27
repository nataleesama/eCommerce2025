using eCommerce.Models;
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
            //NotifyPropertyChanged(nameof(Cart));
        }
        public ObservableCollection<Product?> Products
        {
            get
            {
                return new ObservableCollection<Product?>(_svc.Products);
            }
        }

        public ObservableCollection<KeyValuePair<Product, int>> Cart
        {
            get
            {
                return new ObservableCollection<KeyValuePair<Product, int>>(_cartsvc.Cart);
            }
        }

        public int AddToCart()
        {
            var id =_cartsvc.AddToCart(SelectedProduct ?? null, 1);
            var item = _svc.Delete(SelectedProduct?.Id ?? 0);
            RefreshLists();
            //NotifyPropertyChanged("Cart");
            return id;
        }
    }
}
