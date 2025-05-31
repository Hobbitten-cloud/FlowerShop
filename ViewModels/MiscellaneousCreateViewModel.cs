using FlowerShop.Persistence.Interfaces;
using FlowerShop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public MiscellaneousCreateViewModel(NavigationStore navigationStore)
        {

        }
    }
}
