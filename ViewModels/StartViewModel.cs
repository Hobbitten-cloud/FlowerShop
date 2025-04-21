using FlowerShop.Models;
using FlowerShop.Persistence;
using FlowerShop.Stores;
using FlowerShop.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlowerShop.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        public ICommand ForwardToFlowerCmd { get; }
        public ICommand ForwardToWreathCmd { get; }
        public ICommand ForwardToMiscellaneousCmd { get; }
        public ICommand ForwardToNoteCmd { get; }

        private readonly NavigationStore _navigationStore;
        public StartViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            ForwardToFlowerCmd = new CommandHandler(() => _navigationStore.CurrentViewModel = new FlowerStorageViewModel(_navigationStore));

            // Disabled for now
            //ForwardToWreathCmd = new CommandHandler(() => _navigationStore.CurrentViewModel = new WreathStorageViewModel(_navigationStore));
            //ForwardToMiscellaneousCmd = new CommandHandler(() => _navigationStore.CurrentViewModel = new MiscellaneousStorageViewModel(_navigationStore));
            //ForwardToNoteCmd = new CommandHandler(() => _navigationStore.CurrentViewModel = new NoteStorageViewModel(_navigationStore));
        }
    }
}
