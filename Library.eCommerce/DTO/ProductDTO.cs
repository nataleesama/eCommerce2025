using eCommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.eCommerce.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int? Quantity { get; set; }

        public double? Price { get; set; }

        public string? Display
        {
            get
            {
                return $"{Id}. {Name} - ${Price}; {Quantity}";
            }
        }
        public ProductDTO()
        {
            Name = string.Empty;
        }

        // Copy Constructor
        public ProductDTO(ProductDTO p)
        {
            Name = p.Name;
            Id = p.Id;
            Quantity = p.Quantity;
            Price = p.Price;
        }

        public ProductDTO(Product p)
        {
            Name = p.Name;
            Id = p.Id;
            Quantity = p.Quantity;
            Price = p.Price;
        }

        public override string ToString()
        {
            return Display ?? string.Empty;
        }
    }
}
