using FlowerShop.Models;
using FlowerShop.Persistence;
using FlowerShop.Stores;
using FlowerShop.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlowerShop.ViewModels
{
    public class ProductEditViewModel : BaseViewModel
    {
        public ICommand SaveProductCmd { get; }
        public ICommand SaveImageCmd { get; }
        public ICommand BackToStorageOverviewCmd { get; }

        public List<ProductLocation> productLocations { get; } = new List<ProductLocation>()
        {
            ProductLocation.Inside,
            ProductLocation.Outside,
            ProductLocation.Both
        };

        public List<ProductSize> productSizes { get; } = new List<ProductSize>()
        {
            ProductSize.Tiny,
            ProductSize.Medium,
            ProductSize.Large,
            ProductSize.Giant
        };

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get
            {
                return _selectedProduct;
            }
            set
            {
                _selectedProduct = value; OnPropertyChanged();
            }
        }

        private string _selectedFilePath;
        public string SelectedFilePath
        {
            get
            {
                return _selectedFilePath;
            }
            set
            {
                _selectedFilePath = value; OnPropertyChanged();
            }
        }

        private IRepo<Product> _productRepo;

        // used for navigation
        private readonly NavigationStore _navigationStore;
        public ProductEditViewModel(NavigationStore navigationStore) : this()
        {
            _productRepo = (ProductRepo)App.RepoReg.Get<Product>("ProductRepo");
            _navigationStore = navigationStore;
        }

        // used when creating a product
        public ProductEditViewModel()
        {
            _productRepo = (ProductRepo)App.RepoReg.Get<Product>("ProductRepo");

            SelectedProduct = new Product();

            SaveImageCmd = new SaveImageCommand(this);
            SaveProductCmd = new CommandHandler(SaveProduct);
            BackToStorageOverviewCmd = new CommandHandler(NavigateBack);
        }

        // used when editing a product
        public ProductEditViewModel(NavigationStore navigationStore, Product product)
        {
            _productRepo = (ProductRepo)App.RepoReg.Get<Product>("ProductRepo");
            _navigationStore = navigationStore;

            SelectedProduct = new Product
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Picture = product.Picture,
                Price = product.Price,
                Size = product.Size,
                Amount = product.Amount,
                Location = product.Location,
            };

            SelectedFilePath = "";

            SaveImageCmd = new SaveImageCommand(this);
            SaveProductCmd = new CommandHandler(SaveProduct);
            BackToStorageOverviewCmd = new CommandHandler(NavigateBack);
        }

        public void NavigateBack()
        {
            _navigationStore.CurrentViewModel = new StorageOverviewViewModel(_navigationStore);
        }

        private void SaveProduct()
        {
            if (!string.IsNullOrEmpty(SelectedFilePath))
            {
                SelectedProduct.Picture = File.ReadAllBytes(SelectedFilePath);
            }

            if (SelectedProduct.Id == 0)
            {
                _productRepo.Add(SelectedProduct);
                MessageBox.Show("Produktet er nu oprettet!", "Oprettet", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _productRepo.Update(SelectedProduct);
                MessageBox.Show("Produktet er blevet opdateret!", "Opdateret", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            NavigateBack();
        }
    }
}
