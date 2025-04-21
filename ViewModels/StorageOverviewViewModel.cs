using FlowerShop.Models;
using FlowerShop.Stores;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using FlowerShop.Persistence;
using System.Runtime.ConstrainedExecution;
using System.Collections.ObjectModel;
using FlowerShop.ViewModels.Commands;
using System.Windows.Input;
using System.Windows;

namespace FlowerShop.ViewModels
{
    public class StorageOverviewViewModel : BaseViewModel
    {
        public ICommand DeleteProductCmd { get; }

        public ICommand EditProductCmd { get; }

        public ICommand ForwardToCreateProductCmd { get; }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get
            {
                return _selectedProduct;
            }
            set
            {
                _selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                OnPropertyChanged(nameof(ProductImage));
            }

        }

        public ImageSource ProductImage
        {
            get
            {
                if (_selectedProduct?.Picture == null || _selectedProduct.Picture.Length <= 4)
                {
                    return null;
                }

                BitmapImage bitmap = new BitmapImage();

                using (MemoryStream ms = new MemoryStream(SelectedProduct.Picture))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                }
                return bitmap;
            }
        }

        private IRepo<Product> _productRepo;
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        private readonly NavigationStore _navigationStore;

        public StorageOverviewViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _productRepo = (ProductRepo)App.RepoReg.Get<Product>("ProductRepo");

            ForwardToCreateProductCmd = new CommandHandler(() => _navigationStore.CurrentViewModel = new ProductEditViewModel(_navigationStore));

            DeleteProductCmd = new CommandHandler(DeleteSelectedProduct);
            EditProductCmd = new CommandHandler(EditSelectedProduct);

            foreach (Product product in _productRepo.GetAll())
            {
                Products.Add(product);
            }
        }

        public void EditSelectedProduct()
        {
            _navigationStore.CurrentViewModel = new ProductEditViewModel(_navigationStore, SelectedProduct);
        }

        public void DeleteSelectedProduct()
        {
            if (SelectedProduct != null)
            {
                Product productToRemove = ((ProductRepo)_productRepo).GetById(SelectedProduct.Id);

                if (MessageBox.Show("Er du sikker på, du vil slette?", "Slet", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (productToRemove != null)
                    {
                        _productRepo.Remove(productToRemove);
                        Products.Remove(SelectedProduct);
                        SelectedProduct = null;
                        MessageBox.Show("Produktet er nu slettet", "Slet", MessageBoxButton.OK);
                    }
                }
            }
        }
    }
}
