using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlowerShop.ViewModels.Commands
{
    public class CreateProductCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) // Checks if the button can be executed or not
        {
            return true;
        }

        public void Execute(object? parameter) // What the button does when clicked on
        {
            if (parameter is StorageViewModel svm)
            {
                svm.AddDefaultProduct();
            }
        }
    }
}
