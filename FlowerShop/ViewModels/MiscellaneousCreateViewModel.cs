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
    public class MiscellaneousCreateViewModel : BaseViewModel, IPictureSelection
    {
        public ICommand SelectImageCommand { get; }
        public ICommand CreateMiscellaneousCommand { get; }
        public ICommand ForwardCommand { get; }
        public ICommand BackwardCommand { get; }

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

        private Miscellaneous _selectedMiscellaneous;
        public Miscellaneous SelectedMiscellaneous
        {
            get
            {
                return _selectedMiscellaneous;
            }
            set
            {
                _selectedMiscellaneous = value; OnPropertyChanged();
            }
        }

        private IRepo<Miscellaneous> _miscellaneousRepo;
        private readonly MiscellaneousService _miscellaneousService;

        public MiscellaneousCreateViewModel(NavigationStore navigationStore)
        {
            _miscellaneousRepo = (MiscellaneousRepo)App.RepoReg.Get<Miscellaneous>("MiscellaneousRepo");
            _miscellaneousService = new MiscellaneousService(_miscellaneousRepo);

            SelectedMiscellaneous = new Miscellaneous();

            CreateMiscellaneousCommand = new CommandHandler(() => { CreateNewMiscellaneous(); });
            ForwardCommand = new NavigateCommand(new NavigationService(navigationStore, () => new MiscellaneousStorageViewModel(navigationStore)));
            BackwardCommand = new NavigateCommand(new NavigationService(navigationStore, () => new MiscellaneousStorageViewModel(navigationStore)));

            SelectImageCommand = new SelectImageCommand(this);

        }

        public void CreateNewMiscellaneous()
        {
            try
            {
                _miscellaneousService.Add(SelectedMiscellaneous, SelectedPicture);
                MessageBox.Show("Diverse er blevet oprettet!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                ForwardCommand.Execute(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl: {ex.Message}", "Fejl", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
