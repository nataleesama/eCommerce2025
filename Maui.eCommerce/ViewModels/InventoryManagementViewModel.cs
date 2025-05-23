﻿using eCommerce.Models;
using Library.eCommerce.DTO;
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
    public class InventoryManagementViewModel : INotifyPropertyChanged
    {
        public ProductDTO? SelectedProduct { get; set;  }
        public string? Query { get; set; }
         
        private ProductServiceProxy _svc = ProductServiceProxy.Current; 

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshProductList()
        {
            NotifyPropertyChanged(nameof(Products));
        }
        public ObservableCollection<ProductDTO?> Products
        {
            get
            {
                var filteredList = _svc.Products.Where(p => p?.Name?.ToLower().Contains(Query?.ToLower() ?? string.Empty) ?? false);
                return new ObservableCollection<ProductDTO?>(filteredList);
            }
        }

        public ProductDTO? Delete()
        {
            var item = _svc.Delete(SelectedProduct?.Id ?? 0) ;
            NotifyPropertyChanged("Products");
            return item; 
        }
    }
}
