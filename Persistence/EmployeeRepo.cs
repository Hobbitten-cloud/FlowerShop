using FlowerShop.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Persistence
{
    public class EmployeeRepo : IRepo<Employee>
    {
        public void Add(Employee item)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAll()
        {
            throw new NotImplementedException();
        }

        public Employee? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Employee item)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee item)
        {
            throw new NotImplementedException();
        }
    }
}
