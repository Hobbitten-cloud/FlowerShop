using FlowerShop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Models
{
    public class Flower
    {
        // Properties below
        public int Id { get; set; }
        public string Name { get; set; }
        public FlowerPotSize PotSize { get; set; }
        public FlowerSize PlantSize { get; set; }
        public double SalePrice { get; set; }
        public double PurchasePrice { get; set; }
        public byte[] Picture { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Constructor
        public Flower()
        {

        }

        public Flower(string name, FlowerPotSize potSize, FlowerSize flowerSize, double salePrice, double purchasePrice, byte[] picture)
        {
            Name = name;
            PotSize = potSize;
            PlantSize = flowerSize;
            SalePrice = salePrice;
            PurchasePrice = purchasePrice;
            Picture = picture;
        }
    }
}
