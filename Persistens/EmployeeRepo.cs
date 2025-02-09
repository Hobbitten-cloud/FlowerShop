using FlowerShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Persistens
{
    public class EmployeeRepo
    {
        private List<Employee> employee = new List<Employee>();

        public EmployeeRepo()
        {
            // Fake Instances of the employees at the firm
            employee.Add(new Employee("Jane", "Jane123", "hej21"));
            employee.Add(new Employee("Gerdt", "Gerdt123", "hej21"));
            employee.Add(new Employee("Bodil", "Bodil123", "hej21"));

        }

        // Returns the employee list
        public List<Employee> GetEmployeeList()
        {
            return employee;
        }

    }
}
