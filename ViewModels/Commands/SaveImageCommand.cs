using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlowerShop.ViewModels.Commands
{
    public class SaveImageCommand : ICommand
    {
        private readonly FlowerEditViewModel _viewModel;

        public SaveImageCommand(FlowerEditViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Image files(*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png | Video files(*.mp4, *.avi) | *.mp4; *.avi";
            
            if (openFileDialog.ShowDialog() == true)
            {
                _viewModel.SelectedFilePath = openFileDialog.FileName;
            }
        }
    }
}
