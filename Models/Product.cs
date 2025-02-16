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
        public string PicturePath { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string Size { get; set; }
        public string Location { get; set; }

        // A static integer that constantly increases whenever a new product is created
        private static int idCount = 0;

        // Id to identify what number a specific product is
        public int Id { get; set; }

        // Constructor of the class product
        public Product()
        {
            // increases the product id count based off instances created and used from this constructor
            Id = idCount++;
        }

        public override string ToString()
        {
            return $"{Id}: {PicturePath}: {ProductName}: {Size}: {Location}:";
        }
    }
}
