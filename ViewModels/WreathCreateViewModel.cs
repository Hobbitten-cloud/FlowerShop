using FlowerShop.Models;
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
    public class WreathCreateViewModel : BaseViewModel, IPictureSelection
    {
        public ICommand SelectImageCommand { get; }
        public ICommand ForwardCommand { get; }
        public ICommand BackwardCommand { get; }
        public ICommand CreateWreathCommand { get; }

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

        private Wreath _selectedWreath;
        public Wreath SelectedWreath
        {
            get
            {
                return _selectedWreath;
            }
            set
            {
                _selectedWreath = value; OnPropertyChanged();
            }
        }

        private IRepo<Wreath> _wreathRepo;
        private readonly WreathService _wreathService;

        public WreathCreateViewModel(NavigationStore navigationStore)
        {
            _wreathRepo = (WreathRepo)App.RepoReg.Get<Wreath>("WreathRepo");
            _wreathService = new WreathService(_wreathRepo);

            SelectedWreath = new Wreath();

            CreateWreathCommand = new CommandHandler(() => { CreateNewWreath(); });
            ForwardCommand = new NavigateCommand(new NavigationService(navigationStore, () => new WreathStorageViewModel(navigationStore)));
            BackwardCommand = new NavigateCommand(new NavigationService(navigationStore, () => new WreathStorageViewModel(navigationStore)));

            SelectImageCommand = new SelectImageCommand(this);
        }

        public void CreateNewWreath()
        {
            try
            {
                _wreathService.Add(SelectedWreath, SelectedPicture);
                MessageBox.Show("Kransen er blevet oprettet!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                ForwardCommand.Execute(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
