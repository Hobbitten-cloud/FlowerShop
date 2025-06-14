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
    public class MiscellaneousStorageViewModel : BaseViewModel
    {
        public ICommand BackwardCommand { get; }
        public ICommand DeleteMiscellaneousCommand { get; }
        public ICommand ForwardToCreateMiscellaneousCommand { get; }
        public ICommand ForwardToEditMiscellaneousCommand { get; }

        private Miscellaneous _selectedMiscellaneous;
        public Miscellaneous SelectedMiscellaneous
        {
            get
            {
                return _selectedMiscellaneous;
            }
            set
            {
                _selectedMiscellaneous = value;
            }
        }

        private IRepo<Miscellaneous> _miscellaneousRepo;
        private readonly MiscellaneousService _miscellaneousService;
        private readonly NavigationStore _navigationStore;
        public ObservableCollection<Miscellaneous> Miscellaneouss { get; set; } = new ObservableCollection<Miscellaneous>();

        public MiscellaneousStorageViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _miscellaneousRepo = (MiscellaneousRepo)App.RepoReg.Get<Miscellaneous>("MiscellaneousRepo");
            _miscellaneousService = new MiscellaneousService(_miscellaneousRepo);

            ForwardToEditMiscellaneousCommand = new CommandHandler(() => _navigationStore.CurrentViewModel = new MiscellaneousEditViewModel(navigationStore, SelectedMiscellaneous), () => SelectedMiscellaneous != null);
            ForwardToCreateMiscellaneousCommand = new NavigateCommand(new NavigationService(navigationStore, () => new MiscellaneousCreateViewModel(navigationStore)));
            DeleteMiscellaneousCommand = new CommandHandler(DeleteSelectedMiscellaneous, () => SelectedMiscellaneous != null);

            LoadMiscellaneous();
        }

        public void LoadMiscellaneous()
        {
            try
            {
                Miscellaneouss.Clear();
                var miscellaneouss = _miscellaneousRepo.GetAll();
                foreach (var miscellaneous in miscellaneouss)
                {
                    Miscellaneouss.Add(miscellaneous);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl under indlæsning af diverse: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DeleteSelectedMiscellaneous()
        {
            if (MessageBox.Show("Er du sikker på, du vil slette? " + SelectedMiscellaneous.Name, "Slet", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    _miscellaneousService.Remove(SelectedMiscellaneous);
                    Miscellaneouss.Remove(SelectedMiscellaneous);
                    SelectedMiscellaneous = null;
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
