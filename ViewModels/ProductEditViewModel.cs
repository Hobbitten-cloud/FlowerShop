using FlowerShop.Models;
using FlowerShop.Persistence;
using FlowerShop.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.ViewModels
{
    public class ProductEditViewModel : BaseViewModel
    {
        private IRepo<Product> _productRepo;

        // used for navigation
        private readonly NavigationStore _navigationStore;
        public ProductEditViewModel(NavigationStore navigationStore) // : this()
        {
            _productRepo = (ProductRepo)App.RepoReg.Get<Product>("ProductRepo");
            _navigationStore = navigationStore;
        }

        //// used when creating a product
        //public CarEditViewModel()
        //{
        //    _productRepo = (ProductRepo)App.RepoReg.Get<Product>("ProductRepo");

        //    SelectedCar = new Car();
        //    SelectImageCommand = new SelectImageCommand(this);
        //    SaveCarCommand = new RelayCommand(SaveCar);
        //}

        //// used when editing a product
        //public CarEditViewModel(NavigationStore navigationStore, Car car)
        //{
        //    _productRepo = (ProductRepo)App.RepoReg.Get<Product>("ProductRepo"); 
        //    _navigationStore = navigationStore;

        //    SelectedCar = new Car
        //    {
        //        Id = car.Id,
        //        ModelName = car.ModelName,
        //        LicensePlate = car.LicensePlate,
        //        Picture = car.Picture,
        //        Status = car.Status
        //    };

        //    SelectedFilePath = "";

        //    SelectImageCommand = new SelectImageCommand(this);
        //    SaveCarCommand = new RelayCommand(SaveCar);
        //}
    }
}
