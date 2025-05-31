using FlowerShop.Models;
using FlowerShop.Persistence;
using FlowerShop.Services.RepoServices;
using FlowerShop.Stores;
using FlowerShop.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using FlowerShop.Services;

namespace FlowerShop.ViewModels
{
    public class WreathStorageViewModel : BaseViewModel
    {
        public ICommand BackwardCommand { get; }
        public ICommand DeleteWreathCommand { get; }
        public ICommand ForwardToCreateWreathCommand { get; }
        public ICommand ForwardToEditWreathCommand { get; }

        private Wreath _selectedWreath;
        public Wreath SelectedWreath
        {
            get
            {
                return _selectedWreath;
            }
            set
            {
                _selectedWreath = value;
            }
        }

        private IRepo<Wreath> _wreathRepo;
        private readonly WreathService _wreathService;
        private readonly NavigationStore _navigationStore;
        public ObservableCollection<Wreath> Wreaths { get; set; } = new ObservableCollection<Wreath>();

        public WreathStorageViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _wreathRepo = (WreathRepo)App.RepoReg.Get<Wreath>("WreathRepo");
            _wreathService = new WreathService(_wreathRepo);

            ForwardToEditWreathCommand = new CommandHandler(() => _navigationStore.CurrentViewModel = new WreathEditViewModel(navigationStore, SelectedWreath), () => SelectedWreath != null);
            DeleteWreathCommand = new CommandHandler(DeleteSelectedWreath, () => SelectedWreath != null);
            ForwardToCreateWreathCommand = new NavigateCommand(new NavigationService(navigationStore, () => new WreathCreateViewModel(navigationStore)));

            LoadWreaths();
        }

        public void LoadWreaths()
        {
            try
            {
                Wreaths.Clear();
                var wreaths = _wreathRepo.GetAll();
                foreach (var wreath in wreaths)
                {
                    Wreaths.Add(wreath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl under indlæsning af kranse: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteSelectedWreath()
        {
            if (MessageBox.Show("Er du sikker på, du vil slette? " + SelectedWreath.Name, "Slet", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    _wreathService.Remove(SelectedWreath);
                    Wreaths.Remove(SelectedWreath);
                    SelectedWreath = null;
                    MessageBox.Show("Kransen er nu blevet slettet", "Slet", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Der opstod en fejl: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
