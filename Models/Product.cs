using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Models
{
    public class Product
    {
        // Properties below
        public int Id { get; set; }
        public string ProductName { get; set; }
        public byte[] Picture { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public string Amount { get; set; }
        public ProductLocation Location { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Constructor of the class product
        public Product()
        {
        }

        public Product(string productName, byte[] picture, double price, string size, string amount, ProductLocation location)
        {
            ProductName = productName;
            Picture = picture;
            Price = price;
            Size = size;
            Amount = amount;
            Location = location;
        }
    }
}
