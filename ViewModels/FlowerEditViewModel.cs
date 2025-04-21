using FlowerShop.Models;
using FlowerShop.Models.Enums;
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
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.ComponentModel;

namespace FlowerShop.ViewModels
{
    public class FlowerEditViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public ICommand SaveFlowerCmd { get; }
        public ICommand SaveImageCmd { get; }
        public ICommand BackToFlowerStorageCmd { get; }

        public List<FlowerSize> flowerSizes { get; } = new List<FlowerSize>()
        {
            FlowerSize.Small,
            FlowerSize.Medium,
            FlowerSize.Large
        };

        public List<FlowerPotSize> flowerPotSizes { get; } = new List<FlowerPotSize>()
        {
            FlowerPotSize.Tiny,
            FlowerPotSize.Small,
            FlowerPotSize.Medium,
            FlowerPotSize.Large,
            FlowerPotSize.Giant
        };

        private FlowerProduct _selectedFlower;
        public FlowerProduct SelectedFlower
        {
            get
            {
                return _selectedFlower;
            }
            set
            {
                _selectedFlower = value; OnPropertyChanged();
                OnPropertyChanged(nameof(FlowerImage));
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

        public ImageSource FlowerImage
        {
            get
            {
                if (_selectedFlower?.Picture == null || _selectedFlower.Picture.Length <= 4)
                {
                    return null;
                }

                BitmapImage bitmap = new BitmapImage();

                using (MemoryStream ms = new MemoryStream(SelectedFlower.Picture))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                }
                return bitmap;
            }
        }

        private IRepo<FlowerProduct> _flowerRepo;

        // used for navigation
        private readonly NavigationStore _navigationStore;
        public FlowerEditViewModel(NavigationStore navigationStore) : this()
        {
            _flowerRepo = (FlowerRepo)App.RepoReg.Get<FlowerProduct>("FlowerRepo");
            _navigationStore = navigationStore;
        }

        // used when creating a flower
        public FlowerEditViewModel()
        {
            _flowerRepo = (FlowerRepo)App.RepoReg.Get<FlowerProduct>("FlowerRepo");

            SelectedFlower = new FlowerProduct();

            SaveImageCmd = new SaveImageCommand(this);
            SaveFlowerCmd = new CommandHandler(SaveFlower);
            BackToFlowerStorageCmd = new CommandHandler(NavigateBack);
        }

        // used when editing a flower
        public FlowerEditViewModel(NavigationStore navigationStore, FlowerProduct flowerProduct)
        {
            _flowerRepo = (FlowerRepo)App.RepoReg.Get<FlowerProduct>("FlowerRepo");
            _navigationStore = navigationStore;

            SelectedFlower = new FlowerProduct
            {
                Id = flowerProduct.Id,
                Name = flowerProduct.Name,
                PotSize = flowerProduct.PotSize,
                PlantSize = flowerProduct.PlantSize,
                SalePrice = flowerProduct.SalePrice,
                PurchasePrice = flowerProduct.PurchasePrice,
                Picture = flowerProduct.Picture,
            };

            SaveImageCmd = new SaveImageCommand(this);
            SaveFlowerCmd = new CommandHandler(SaveFlower);
            BackToFlowerStorageCmd = new CommandHandler(NavigateBack);
        }

        public void NavigateBack()
        {
            _navigationStore.CurrentViewModel = new FlowerStorageViewModel(_navigationStore);
        }

        private void SaveFlower()
        {
            if (!string.IsNullOrEmpty(SelectedFilePath))
            {
                SelectedFlower.Picture = File.ReadAllBytes(SelectedFilePath);
            }

            if (SelectedFlower.Id == 0)
            {
                _flowerRepo.Add(SelectedFlower);
                MessageBox.Show("Blomsten er nu oprettet!", "Oprettet", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _flowerRepo.Update(SelectedFlower);
                MessageBox.Show("Blomsten er blevet opdateret!", "Opdateret", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            NavigateBack();
        }
    }
}
