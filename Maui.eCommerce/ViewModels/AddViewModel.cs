using eCommerce.Models;
using Library.eCommerce.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class AddViewModel : INotifyPropertyChanged
    {
        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    NotifyPropertyChanged(nameof(Message));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string message = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(message));
        }
        public string? Name
        {
            get
            {
                return Model?.Name ?? string.Empty;
            }
            set
            {
                if(Model != null && Model.Name != value)
                {
                    Model.Name = value;
                }
            }
        }

        public double? Price
        {
            get
            {
                return Model?.Price ?? null;
            }
            set
            {
                if(Model != null && Model.Price != value)
                {
                    double valueToTenth = Math.Round(value ?? 0, 2);
                    Model.Price = valueToTenth;
                }
            }
        }

        public int? Quantity
        {
            get
            {
                return Model?.Quantity ?? null;
            }
            set
            {
                if (Model != null && Model.Quantity != value)
                {
                    Model.Quantity = value;
                }
            }
        }

        public Product? Model { get; set; }

        public bool AddOrUpdate()
        {
            if(Model.Name != String.Empty && Model.Name is string &&
                Model.Price != null && Model.Price is double &&
                Model.Quantity != null && Model.Quantity is int)
            {
                ProductServiceProxy.Current.AddOrUpdate(Model);
                return true;
            }
            Message = "Please fill out all fields with the appropriate values";
            return false;
        }

        public AddViewModel()
        {
            Model = new Product();
        }

        public AddViewModel(Product? model)
        {
            Model = model;
        }
    }
}
