using eCommerce.Models;
using Library.eCommerce.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.eCommerce.ViewModels
{
    public class InventoryManagementViewModel
    {
        public List<Product?> Products
        {
            get
            {
                return ProductServiceProxy.Current.Products;
            }
        }
    }
}
