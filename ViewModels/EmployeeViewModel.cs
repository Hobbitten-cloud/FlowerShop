using FlowerShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.ViewModels
{
    public class EmployeeViewModel
    {
        // Properties below
        public string Username { get; set; }
        public string Password { get; set; }

        // Constructor of EmployeeViewModel It takes the Employee as a parameter and refers to 1 employee
        public EmployeeViewModel(Employee employee)
        {
            Username = employee.Username;
            Password = employee.Password;
        }

    }
}
