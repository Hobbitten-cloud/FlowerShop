using FlowerShop.Models;
using FlowerShop.Persistence;
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
    public class LoginViewModel
    {
        // Command binds the loginCmd to the LoginCommand class
        public ICommand LoginCmd { get; } = new LoginCommand();
        public string Username { get; set; }
        public string Password { get; set; }

        // Employee list
        public EmployeeRepo employeeRepo = new EmployeeRepo();
        public List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();

        // Constructor of our MainViewModel
        public LoginViewModel()
        {
            // Calls our function to retrieve all users from the database
            employeeRepo.GetAll();

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
    }
}
