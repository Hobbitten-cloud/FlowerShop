using FlowerShop.Models;
using FlowerShop.Models.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShop.Persistence
{
    public class MiscellaneousRepo : IRepo<Miscellaneous>
    {
        private readonly string ConnectionString;
        private List<Miscellaneous> _miscellaneousProducts;

        public MiscellaneousRepo()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _miscellaneousProducts = new List<Miscellaneous>();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Flower flowerProducts)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO FlowerProduct (Name, PotSize, PlantSize, SalePrice, PurchasePrice, Picture)" +
                                                       "VALUES(@Name, @PotSize, @PlantSize, @SalePrice, @PurchasePrice, @Picture)" +
                                                       "SELECT @@IDENTITY", con))
                {
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = flowerProducts.Name;
                    cmd.Parameters.Add("@PotSize", SqlDbType.NVarChar).Value = flowerProducts.PotSize;
                    cmd.Parameters.Add("@PlantSize", SqlDbType.NVarChar).Value = flowerProducts.PlantSize;
                    cmd.Parameters.Add("@SalePrice", SqlDbType.Float).Value = flowerProducts.SalePrice;
                    cmd.Parameters.Add("@PurchasePrice", SqlDbType.Float).Value = flowerProducts.PurchasePrice;
                    cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = flowerProducts.Picture;

                    flowerProducts.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    _flowerProducts.Add(flowerProducts);
                }
            }
        }

        public List<Flower> GetAll()
        {
            List<Flower> flowerProducts = new List<Flower>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, PotSize, PlantSize, SalePrice, PurchasePrice, Picture, IsDeleted FROM FlowerProduct WHERE IsDeleted = 0", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Flower flowerProduct = new Flower
                        {
                            Id = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            PotSize = (FlowerPotSize)Enum.Parse(typeof(FlowerPotSize), dr.GetString(2)),
                            PlantSize = (FlowerSize)Enum.Parse(typeof(FlowerSize), dr.GetString(3)),
                            SalePrice = dr.GetDouble(4),
                            PurchasePrice = dr.GetDouble(5),
                            Picture = (byte[])dr.GetSqlBinary(6).Value,
                            IsDeleted = dr.GetBoolean(7)
                        };
                        flowerProducts.Add(flowerProduct);
                    }
                }
            }
            return flowerProducts;
        }

        public Flower? GetById(int id)
        {
            Flower? flowerProduct = null;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, PotSize, PlantSize, SalePrice, PurchasePrice, Picture, IsDeleted FROM FlowerProduct WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        flowerProduct = new Flower
                        {
                            Id = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            PotSize = (FlowerPotSize)Enum.Parse(typeof(FlowerPotSize), dr.GetString(2)),
                            PlantSize = (FlowerSize)Enum.Parse(typeof(FlowerSize), dr.GetString(3)),
                            SalePrice = dr.GetDouble(4),
                            PurchasePrice = dr.GetDouble(5),
                            Picture = (byte[])dr.GetSqlBinary(6).Value,
                            IsDeleted = dr.GetBoolean(7)
                        };
                    }
                }
            }
            return flowerProduct;
        }

        public void Remove(Flower flowerProduct)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE FlowerProduct SET IsDeleted = 1 WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", flowerProduct.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Flower flowerProducts)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE FlowerProduct SET Name = @Name, PotSize = @PotSize, " +
                    "PlantSize = @PlantSize, SalePrice = @SalePrice, PurchasePrice = @PurchasePrice, Picture = @Picture WHERE Id = @Id", con);

                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = flowerProducts.Id;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = flowerProducts.Name;
                cmd.Parameters.Add("@PotSize", SqlDbType.NVarChar).Value = flowerProducts.PotSize;
                cmd.Parameters.Add("@PlantSize", SqlDbType.NVarChar).Value = flowerProducts.PlantSize;
                cmd.Parameters.Add("@SalePrice", SqlDbType.Float).Value = flowerProducts.SalePrice;
                cmd.Parameters.Add("@PurchasePrice", SqlDbType.Float).Value = flowerProducts.PurchasePrice;
                cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = flowerProducts.Picture;
                cmd.ExecuteNonQuery();
            }
        }
    }
}