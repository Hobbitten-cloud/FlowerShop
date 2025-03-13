using FlowerShop.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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

        // Reads the database
        private readonly string ConnectionString;

        public EmployeeRepo()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) // Ensures relative path works
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();

            // Retrieves all of our Owners and addes them to our list
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT EmployeeId, Name, Username, Password FROM EMPLOYEE ORDER BY EmployeeId", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Employee employee = new Employee(dr.GetInt32(0))
                        {
                            Name = dr.GetString(1),
                            Username = dr.GetString(2),
                            Password = dr.GetString(3)
                        };
                        employees.Add(employee);
                    }
                }
            }
            return employees;
        }

        // Returns the employee list
        public List<Employee> GetEmployeeList()
        {
            return employee;
        }

    }
}
