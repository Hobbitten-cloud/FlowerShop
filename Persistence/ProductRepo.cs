using FlowerShop.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Persistence
{
    public class ProductRepo : IRepo<Product>
    {
        private readonly string ConnectionString;
        private List<Product> _products;

        public ProductRepo()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            _products = new List<Product>();

            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Product product)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Product (ProductName, Picture, Price, Size, Amount, Location)" +
                                                       "VALUES(@ProductName, @Picture, @Price, @Size, @Location)" +
                                                       "SELECT @@IDENTITY", con))
                {
                    cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = product.ProductName;
                    cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = product.Picture;
                    cmd.Parameters.Add("@Price", SqlDbType.Float).Value = product.Price;
                    cmd.Parameters.Add("@Size", SqlDbType.NVarChar).Value = product.Size;
                    cmd.Parameters.Add("@Amount", SqlDbType.NVarChar).Value = product.Amount;
                    cmd.Parameters.Add("@Location", SqlDbType.NVarChar).Value = product.Location;

                    product.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    _products.Add(product);
                }
            }
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, ProductName, Picture, Price, Size, Amount, Location, IsDeleted FROM Product", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Product product = new Product
                        {
                            Id = dr.GetInt32(0),
                            ProductName = dr.GetString(1),
                            Picture = (byte[])dr.GetSqlBinary(2).Value,
                            Price = dr.GetDouble(3),
                            Size = dr.GetString(4),
                            Amount = dr.GetString(5),
                            Location = (ProductLocation)Enum.Parse(typeof(ProductLocation), dr.GetString(6)),
                            IsDeleted = dr.GetBoolean(7)
                        };
                        products.Add(product);
                    }
                }
            }
            return products;
        }

        public Product? GetById(int id)
        {
            Product? product = null;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, ProductName, Picture, Price, Size, Amount, Location, IsDeleted FROM Product WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        product = new Product
                        {
                            Id = dr.GetInt32(0),
                            ProductName = dr.GetString(1),
                            Picture = (byte[])dr.GetSqlBinary(2).Value,
                            Price = dr.GetDouble(3),
                            Size = dr.GetString(4),
                            Amount = dr.GetString(5),
                            Location = (ProductLocation)Enum.Parse(typeof(ProductLocation), dr.GetString(6)),
                            IsDeleted = dr.GetBoolean(7)
                        };
                    }
                }
            }
            return product;
        }

        public void Remove(Product product)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Product SET IsDeleted = 1 WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", product.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Product product)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Product SET ProductName = @ProductName, Picture = @Picture, " +
                    "Price = @Price, Size = @Size, Amount = @Amount, Location = @Location WHERE Id = @Id", con);

                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = product.Id;
                cmd.Parameters.Add("@ProductName", SqlDbType.NVarChar).Value = product.ProductName;
                cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = product.Picture;
                cmd.Parameters.Add("@Price", SqlDbType.Float).Value = product.Price;
                cmd.Parameters.Add("@Size", SqlDbType.NVarChar).Value = product.Size;
                cmd.Parameters.Add("@Amount", SqlDbType.NVarChar).Value = product.Amount;
                cmd.Parameters.Add("@Location", SqlDbType.NVarChar).Value = product.Location;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
