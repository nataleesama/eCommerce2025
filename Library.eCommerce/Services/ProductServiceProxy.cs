﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Models;

namespace Library.eCommerce.Services
{
    public class ProductServiceProxy
    {
        private ProductServiceProxy()
        {
            Products = new List<Product?>
            {
                new Product{Id=1, Name = "Product 1", Price = 15.99, Quantity = 5 },
                new Product{Id=2, Name = "Product 2", Price = 12.99, Quantity = 3 },
                new Product{Id=3, Name = "Product 3", Price = 20.99, Quantity = 2 }
            };
        }

        private int LastKey
        {
            get
            {
                if (!Products.Any())
                {
                    return 0;
                }
                return Products.Select(p => p?.Id ?? 0).Max();
            }
        }

        private static ProductServiceProxy? instance;
        private static object instanceLock = new object();

        public static ProductServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductServiceProxy();
                    }
                }

                return instance;
            }
        }

        public List<Product?> Products { get; private set; }

        public Product AddOrUpdate(Product product)
        {
            if (product.Id == 0)
            {
                product.Id = LastKey + 1;
                Products.Add(product);
            }
            else
            {
                var existingItem = Products.FirstOrDefault(p => p.Id == product.Id);
                Products.Remove(existingItem);
                Products.Add(new Product(product));
            }

                return product;
        }

        public Product? Delete(int id)
        {
            if (id == 0)
            {
                return null;
            }
            Product? product = Products.FirstOrDefault(p => p.Id == id);
            Products.Remove(product);
            return product;
        }

        public Product? GetById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }
    }
}
