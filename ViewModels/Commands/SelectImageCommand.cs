using FlowerShop.Persistence.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlowerShop.ViewModels.Commands
{
    public class SelectImageCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly IPictureSelection _viewModel;

        public SelectImageCommand(IPictureSelection viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

            if (ofd.ShowDialog() == true)
            {
                _viewModel.SelectedPicture = ofd.FileName;
            }
        }
    }
}