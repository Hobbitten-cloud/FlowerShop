using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public EmployeeTitle Title { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Employee(string name, string username, string password, string phoneNumber, EmployeeTitle title)
        {
            Name = name;
            Username = username;
            Password = password;
            PhoneNumber = phoneNumber;
            Title = title;
        }
    }
}
