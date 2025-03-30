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
    public class ReceiptViewModel : INotifyPropertyChanged
    {
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

        public double PreTax
        {
            get
            {
                return Math.Round(_cartsvc.GetTotal(), 2);
            }
        }

        public double Tax
        {
            get
            {
                return Math.Round(_cartsvc.GetTotal() * .07, 2);
            }
        }

        public double PostTax
        {
            get
            {
                return Math.Round(_cartsvc.GetTotal() * 1.07,2);
            }
        }

        public ObservableCollection<ProductInCart?> Cart
        {
            get
            {
                return new ObservableCollection<ProductInCart?>(_cartsvc.Cart);
            }
        }

        public void RefreshReceipt() 
        {
            NotifyPropertyChanged(nameof(Cart));
            NotifyPropertyChanged(nameof(PreTax));
            NotifyPropertyChanged(nameof(Tax));
            NotifyPropertyChanged(nameof(PostTax));
        }

        public void DropCart()
        {
            _cartsvc.Drop();
        }
    }
}
