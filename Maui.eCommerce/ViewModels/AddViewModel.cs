using eCommerce.Models;
using Library.eCommerce.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class AddViewModel
    {
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
            if(Model.Name != String.Empty &&
                Model.Price != null &&
                Model.Quantity != null)
            {
                ProductServiceProxy.Current.AddOrUpdate(Model);
                return true;
            }
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
