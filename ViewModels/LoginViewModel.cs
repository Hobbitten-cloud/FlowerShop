using FlowerShop.Models;
using FlowerShop.Persistens;
using FlowerShop.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlowerShop.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        // Command binds the loginCmd to the LoginCommand class
        public ICommand LoginCmd { get; } = new LoginCommand();
        public string Username { get; set; }
        public string Password { get; set; }

        // Employee list
        EmployeeRepo employeeRepo = new EmployeeRepo();
        public List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();

        // Constructor of our MainViewModel
        public LoginViewModel()
        {
            foreach (Employee employee in employeeRepo.GetEmployeeList())
            {
                employeeViewModels.Add(new EmployeeViewModel(employee));
            }
        }

        // Checks if the employee exists in the employee list
        public EmployeeViewModel? EmployeeLogin(string employeeUsername, string employeePassword)
        {
            foreach (EmployeeViewModel employeeVM in employeeViewModels)
            {
                if (employeeVM.Username == employeeUsername && employeeVM.Password == employeePassword)
                {
                    return employeeVM;
                }
            }
            return null;
        }

        // Property event
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
