using FlowerShop.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Models
{
    public class Wreath
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double PurchasePrice { get; set; }
        public double SalePrice { get; set; }
        public WreathSize Size { get; set; }
        public int Amount { get; set; }
        public byte[] Picture { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Wreath()
        {

        }

        public Wreath(string name, double purchasePrice, double salePrice, WreathSize size, int amount, byte[] picture)
        {
            Name = name;
            PurchasePrice = purchasePrice;
            SalePrice = salePrice;
            Size = size;
            Amount = amount;
            Picture = picture;
        }
    }
}
