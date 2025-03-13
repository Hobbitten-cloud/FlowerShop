using FlowerShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Persistence
{
    public class ProductRepo
    {
        // The list of products
        private List<Product> products = new List<Product>();

        // Once ProductRepo is called it reads the database
        public ProductRepo()
        {
            // Loads the text file
            //ReadTxtFile();
        }
        public List<Product> GetAll()
        {
            return products;
        }

        //// Reads the .txt file that was created and makes the information from there into data that the program can use
        //private void ReadTxtFile()
        //{
        //    // Detects if a file is not yet created with the name "Products.txt"
        //    if (!File.Exists("Products.txt"))
        //    {
        //        using (StreamWriter sw = File.CreateText("Products.txt"))
        //        {

        //        }
        //    }
        //    // Wether the above code is called or not it will attempt to read the .txt here
        //    try
        //    {
        //        using (StreamReader sr = new StreamReader("Products.txt"))
        //        {
        //            string line = sr.ReadLine();

        //            while (line != null)
        //            {
        //                string[] parts = line.Split(';');

        //                Add(parts[0], parts[1], parts[2], parts[3], double.Parse(parts[4]), parts[5],
        //                    double.Parse(parts[6]), double.Parse(parts[7]), double.Parse(parts[8]),
        //                    double.Parse(parts[9]), int.Parse(parts[10]), int.Parse(parts[11]));

        //                line = sr.ReadLine();
        //            }
        //        }
        //    }
        //    catch (IOException)
        //    {
        //        throw;
        //    }
        //}

        // Saves the products to a .txt file
        //public void SaveTxtFile()
        //{
        //    using (StreamWriter sw = new StreamWriter("Products.txt"))
        //    {
        //        foreach (Product product in products)
        //        {
        //            sw.WriteLine($"{product.PicturePath};{product.ForSale};{product.ProductName};" +
        //                $"{product.Size};{product.Weight};{product.Location};{product.SalePriceWithCom};" +
        //                $"{product.SalePriceWithoutCom};{product.BuyPriceWithCom};{product.BuyPriceWithoutCom};" +
        //                $"{product.Sold};{product.QuantityInStock}");
        //        }
        //    }
        //}

        // Gets 1 specific product
        //public Product Get(int id)
        //{
        //    Product result = null;

        //    foreach (Product product in products)
        //    {
        //        if (product.Id == id)
        //        {
        //            result = product;
        //            break;
        //        }
        //    }
        //    return result;
        //}

        // Gets the product list

        //// Updates the products
        //public void Update(int Id, string picturePath, string forSale, string productName,
        //               string size, double weight, string location,
        //               double salePriceWithCom, double salePriceWithoutCom,
        //               double buyPriceWithCom, double buyPriceWithoutCom,
        //               int sold, int quantityInStock)
        //{
        //    Product product = Get(Id);

        //    product.PicturePath = picturePath;
        //    product.ForSale = forSale;
        //    product.ProductName = productName;
        //    product.Size = size;
        //    product.Weight = weight;
        //    product.Location = location;
        //    product.SalePriceWithCom = salePriceWithCom;
        //    product.SalePriceWithoutCom = salePriceWithoutCom;
        //    product.BuyPriceWithCom = buyPriceWithCom;
        //    product.BuyPriceWithoutCom = buyPriceWithoutCom;
        //    product.Sold = sold;
        //    product.QuantityInStock = quantityInStock;
        //}

        // Adds a product 
        //public Product Add(string picturePath, string forSale, string productName,
        //               string size, double weight, string location,
        //               double salePriceWithCom, double salePriceWithoutCom,
        //               double buyPriceWithCom, double buyPriceWithoutCom,
        //               int sold, int quantityInStock)
        //{
        //    // The code here will work without null but if null was not where it could return something that wouldnt exist and cause the program to crash
        //    // And because we throw our own exception we can control it and if we didnt have that exception it would throw an exception that we dont control
        //    Product result = null;

        //    if (!string.IsNullOrEmpty(picturePath) && !string.IsNullOrEmpty(forSale)
        //        && !string.IsNullOrEmpty(productName) && !string.IsNullOrEmpty(size) &&
        //        weight <= 0.0 && !string.IsNullOrEmpty(location) &&
        //        salePriceWithCom <= 0.0 && salePriceWithoutCom <= 0.0 &&
        //        buyPriceWithCom <= 0.0 && buyPriceWithoutCom <= 0.0 &&
        //        sold <= 0 && quantityInStock <= 0)
        //    {

        //        result = new Product()
        //        {
        //            PicturePath = picturePath,
        //            ForSale = forSale,
        //            ProductName = productName,
        //            Size = size,
        //            Weight = weight,
        //            Location = location,
        //            SalePriceWithCom = salePriceWithCom,
        //            SalePriceWithoutCom = salePriceWithoutCom,
        //            BuyPriceWithCom = buyPriceWithCom,
        //            BuyPriceWithoutCom = buyPriceWithoutCom,
        //            Sold = sold,
        //            QuantityInStock = quantityInStock
        //        };
        //        products.Add(result);
        //    }
        //    else
        //    {
        //        throw (new ArgumentException("Not all arguments are valid!"));
        //    }
        //    return result;
        //}

        // Removes a product
        //public void Remove(int id)
        //{
        //    Product foundProduct = Get(id);
        //    if (foundProduct != null)
        //    {
        //        products.Remove(foundProduct);
        //    }
        //    else
        //    {
        //        throw (new ArgumentException("Product with id = " + id + " not found"));
        //    }
        //}
    }
}
