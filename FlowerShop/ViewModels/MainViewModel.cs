﻿using FlowerShop.Services;
using FlowerShop.Stores;
using FlowerShop.ViewModels.Commands;
using FlowerShop.ViewModels.TestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlowerShop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand ForwardToStartCommand { get; }
        public ICommand ForwardToFlowerCommand { get; }
        public ICommand ForwardToWreathCommand { get; }
        public ICommand ForwardToMiscellaneousCommand { get; }
        public ICommand ForwardToSettlementCommand { get; }
        public ICommand ForwardToNoteCommand { get; }
        public ICommand QuitProgramCommand { get; }

        private readonly NavigationStore _navigationStore;

        public BaseViewModel CurrentViewModel { get => _navigationStore.CurrentViewModel; }

        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            ForwardToStartCommand = new NavigateCommand(new NavigationService(navigationStore, () => new StartViewModel(navigationStore)));
            ForwardToFlowerCommand = new NavigateCommand(new NavigationService(navigationStore, () => new FlowerStorageViewModel(navigationStore)));
            ForwardToWreathCommand = new NavigateCommand(new NavigationService(navigationStore, () => new WreathStorageViewModel(navigationStore)));
            ForwardToMiscellaneousCommand = new NavigateCommand(new NavigationService(navigationStore, () => new MiscellaneousStorageViewModel(navigationStore)));
            ForwardToSettlementCommand = new NavigateCommand(new NavigationService(navigationStore, () => new TestViewModel(navigationStore)));
            ForwardToNoteCommand = new NavigateCommand(new NavigationService(navigationStore, () => new NoteStorageViewModel(navigationStore)));
            QuitProgramCommand = new CommandHandler(QuitProgram);
        }

        private void QuitProgram()
        {
            Environment.Exit(0);
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
