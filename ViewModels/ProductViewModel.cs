using FlowerShop.Models;
using FlowerShop.Persistens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.ViewModels
{
    public class ProductViewModel
    {
        // A private instance of product
        private Product products;

        // ViewModel informations
        public string PicturePath { get; set; }
        public string ForSale { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public double Weight { get; set; }
        public string Location { get; set; }
        public double SalePriceWithCom { get; set; }
        public double SalePriceWithoutCom { get; set; }
        public double BuyPriceWithCom { get; set; }
        public double BuyPriceWithoutCom { get; set; }
        public int Sold { get; set; }
        public int QuantityInStock { get; set; }

        // Constructor of our ProductViewModel
        public ProductViewModel(Product products)
        {
            this.products = products;

            PicturePath = products.PicturePath;
            ForSale = products.ForSale;
            ProductName = products.ProductName;
            Size = products.Size;
            Weight = products.Weight;
            Location = products.Location;
            SalePriceWithCom = products.SalePriceWithCom;
            SalePriceWithoutCom = products.SalePriceWithoutCom;
            BuyPriceWithCom = products.BuyPriceWithCom;
            BuyPriceWithoutCom = products.BuyPriceWithoutCom;
            Sold = products.Sold;
            QuantityInStock = products.QuantityInStock;
        }

        // Deletes the product at the given position
        public void DeleteProduct(ProductRepo productRepo)
        {
            productRepo.Remove(products.Id);
        }

        // Updates the product
        public void UpdateProduct(ProductRepo productRepo)
        {
            productRepo.Update
                (
                    products.Id, PicturePath, ForSale, ProductName, Size, Weight, Location,
                    SalePriceWithCom, SalePriceWithoutCom, BuyPriceWithCom,
                    BuyPriceWithoutCom, Sold, QuantityInStock
                );
        }
    }
}
