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
    public class FlowerStorageViewModel : BaseViewModel
    {
        public ICommand DeleteFlowerCmd { get; }

        public ICommand EditFlowerCmd { get; }

        public ICommand ForwardToCreateFlowerCmd { get; }
        public ICommand BackToStartCmd { get; }

        private Flower _selectedFlower;
        public Flower SelectedFlower
        {
            get
            {
                return _selectedFlower;
            }
            set
            {
                _selectedFlower = value;
                OnPropertyChanged(nameof(SelectedFlower));
                OnPropertyChanged(nameof(FlowerImage));
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

        private IRepo<Flower> _flowerRepo;
        public ObservableCollection<Flower> Flowers { get; set; } = new ObservableCollection<Flower>();

        private readonly NavigationStore _navigationStore;

        public FlowerStorageViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _flowerRepo = (FlowerRepo)App.RepoReg.Get<Flower>("FlowerRepo");

            DeleteFlowerCmd = new CommandHandler(DeleteSelectedFlower);
            EditFlowerCmd = new CommandHandler(EditSelectedFlower);

            ForwardToCreateFlowerCmd = new CommandHandler(() => _navigationStore.CurrentViewModel = new FlowerEditViewModel(_navigationStore));
            BackToStartCmd = new CommandHandler(() => _navigationStore.CurrentViewModel = new StartViewModel(_navigationStore));

            foreach (Flower flower in _flowerRepo.GetAll())
            {
                Flowers.Add(flower);
            }
        }

        public void EditSelectedFlower()
        {
            if (SelectedFlower != null)
            {
                _navigationStore.CurrentViewModel = new FlowerEditViewModel(_navigationStore, SelectedFlower);
            }
            else
            {
                MessageBox.Show("Du skal vælge en blomst først!", "Fejl!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void DeleteSelectedFlower()
        {
            if (SelectedFlower != null)
            {
                Flower flowerToRemove = ((FlowerRepo)_flowerRepo).GetById(SelectedFlower.Id);

                if (MessageBox.Show("Er du sikker på, du vil slette?", "Slet", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (flowerToRemove != null)
                    {
                        _flowerRepo.Remove(flowerToRemove);
                        Flowers.Remove(SelectedFlower);
                        SelectedFlower = null;
                        MessageBox.Show("Produktet er nu slettet", "Slet", MessageBoxButton.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Du skal vælge en blomst først!", "Fejl!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
