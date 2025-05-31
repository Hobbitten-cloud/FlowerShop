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
using FlowerShop.Services.RepoServices;
using FlowerShop.Services;

namespace FlowerShop.ViewModels
{
    public class FlowerStorageViewModel : BaseViewModel
    {
        public ICommand BackwardCommand { get; }
        public ICommand DeleteFlowerCommand { get; }
        public ICommand ForwardToCreateFlowerCommand { get; }
        public ICommand ForwardToEditFlowerCommand { get; }

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
            }
        }

        private IRepo<Flower> _flowerRepo;
        private readonly FlowerService _flowerService;
        private readonly NavigationStore _navigationStore;
        public ObservableCollection<Flower> Flowers { get; set; } = new ObservableCollection<Flower>();

        public FlowerStorageViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _flowerRepo = (FlowerRepo)App.RepoReg.Get<Flower>("FlowerRepo");
            _flowerService = new FlowerService(_flowerRepo);

            ForwardToEditFlowerCommand = new CommandHandler(() => _navigationStore.CurrentViewModel = new FlowerEditViewModel(navigationStore, SelectedFlower), () => SelectedFlower != null);
            DeleteFlowerCommand = new CommandHandler(DeleteSelectedFlower, () => SelectedFlower != null);
          
            ForwardToCreateFlowerCommand = new NavigateCommand(new NavigationService(navigationStore, () => new FlowerCreateViewModel(navigationStore)));

            LoadFlowers();
        }

        public void LoadFlowers()
        {
            try
            {
                Flowers.Clear();
                var flowers = _flowerRepo.GetAll();
                foreach (var flower in flowers)
                {
                    Flowers.Add(flower);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl under indlæsning af blomster: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteSelectedFlower()
        {
            if (MessageBox.Show("Er du sikker på, du vil slette? " + SelectedFlower.Name, "Slet", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    _flowerService.Remove(SelectedFlower);
                    Flowers.Remove(SelectedFlower);
                    SelectedFlower = null;
                    MessageBox.Show("Blomsten er nu blevet slettet", "Slet", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Der opstod en fejl: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
