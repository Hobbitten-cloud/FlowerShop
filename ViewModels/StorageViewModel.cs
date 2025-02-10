using FlowerShop.Models;
using FlowerShop.Persistens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.ViewModels
{
    public class StorageViewModel : INotifyPropertyChanged
    {
        public StorageViewModel()
        {
            foreach (Product products in productRepo.GetAll())
            {
                ProductViewModel currentProductVM = new ProductViewModel(products);
                ProductVM.Add(currentProductVM);
            }
        }

        // Create product button
        public void AddDefaultProduct()
        {
            Product products = productRepo.Add("Tilføj billedsti her", "Ja / Nej", "Angiv Produkt Navn", "Angiv Størrelse", 0.0, "Angiv udenfor / indendørs", 0.0, 0.0, 0.0, 0.0, 0, 0);
            ProductViewModel productViewModel = new ProductViewModel(products);
            ProductVM.Add(productViewModel);
            SelectedProduct = productViewModel;
        }

        // Delete product button
        public void DeleteSelectedProduct()
        {
            if (SelectedProduct != null)
            {
                SelectedProduct.DeleteProduct(productRepo);
                ProductVM.Remove(SelectedProduct);
                SelectedProduct = ProductVM.LastOrDefault();
            }
        }

        // Saves all products
        public void SaveAllProducts()
        {
            foreach (ProductViewModel mvm in ProductVM)
            {
                mvm.UpdateProduct(productRepo);
            }
            productRepo.SaveTxtFile();
        }

        // Property event
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
