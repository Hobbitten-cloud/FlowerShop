using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Models
{
    public class Miscellaneous
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double PurchasePrice { get; set; }
        public int Amount { get; set; }
        public byte[] Picture { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Miscellaneous()
        {

        }

        public Miscellaneous(string name, double purchasePrice, int amount, byte[] picture)
        {
            Name = name;
            PurchasePrice = purchasePrice;
            Amount = amount;
            Picture = picture;
        }
    }
}
