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

        public Product? Model { get; set; }

        public void AddOrUpdate()
        {
            ProductServiceProxy.Current.AddOrUpdate(Model);
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
