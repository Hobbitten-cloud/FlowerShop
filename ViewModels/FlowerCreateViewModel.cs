using FlowerShop.Models;
using FlowerShop.Models.Enums;
using FlowerShop.Persistence;
using FlowerShop.Persistence.Interfaces;
using FlowerShop.Services;
using FlowerShop.Services.RepoServices;
using FlowerShop.Stores;
using FlowerShop.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FlowerShop.ViewModels
{
    public class FlowerCreateViewModel : BaseViewModel, IPictureSelection
    {
        public ICommand SelectImageCommand { get; }
        public ICommand ForwardCommand { get; }
        public ICommand BackwardCommand { get; }
        public ICommand CreateFlowerCommand { get; }

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

        private string _selectedPicture;
        public string SelectedPicture
        {
            get
            {
                return _selectedPicture;
            }
            set
            {
                _selectedPicture = value; OnPropertyChanged();
            }
        }

        private Flower _selectedFlower;
        public Flower SelectedFlower
        {
            get
            {
                return _selectedFlower;
            }
            set
            {
                _selectedFlower = value; OnPropertyChanged();
            }
        }

        private IRepo<Flower> _flowerRepo;
        private readonly FlowerService _flowerService;

        public FlowerCreateViewModel(NavigationStore navigationStore)
        {
            _flowerRepo = (FlowerRepo)App.RepoReg.Get<Flower>("FlowerRepo");
            _flowerService = new FlowerService(_flowerRepo);


            SelectedFlower = new Flower();

            CreateFlowerCommand = new CommandHandler(() => { CreateNewFlower();});
            ForwardCommand = new NavigateCommand(new NavigationService(navigationStore, () => new FlowerStorageViewModel(navigationStore)));
            BackwardCommand = new NavigateCommand(new NavigationService(navigationStore, () => new FlowerStorageViewModel(navigationStore)));

            SelectImageCommand = new SelectImageCommand(this);
        }

        public void CreateNewFlower()
        {
            try
            {
                _flowerService.Add(SelectedFlower, SelectedPicture);
                MessageBox.Show("Blomsten er blevet oprettet!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                ForwardCommand.Execute(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
