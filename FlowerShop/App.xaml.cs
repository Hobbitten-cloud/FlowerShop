﻿using FlowerShop.Models;
using FlowerShop.Persistence;
using FlowerShop.Stores;
using FlowerShop.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace FlowerShop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // The repository registry to manage different repositories
        public static RepoRegistry RepoReg { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Initialize the repository registry
            RepoReg = new RepoRegistry();

            // Repository registrations
            RepoReg.Register<Flower>("FlowerRepo", new FlowerRepo());
            RepoReg.Register<Miscellaneous>("MiscellaneousRepo", new MiscellaneousRepo());
            RepoReg.Register<Note>("NoteRepo", new NoteRepo());
            RepoReg.Register<Wreath>("WreathRepo", new WreathRepo());

            // Initialize the navigation store and set the initial view model
            NavigationStore navigationStore = new NavigationStore();
            navigationStore.CurrentViewModel = new StartViewModel(navigationStore);

            // Initialize the main window and set its data context
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
