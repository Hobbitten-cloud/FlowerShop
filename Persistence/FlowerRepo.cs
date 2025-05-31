using FlowerShop.Models;
using FlowerShop.Models.Enums;
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
    public class FlowerRepo : IRepo<Flower>
    {
        private readonly string ConnectionString;
        private List<Flower> _flowerProducts;

        public FlowerRepo()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _flowerProducts = new List<Flower>();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Flower flower)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Flower (Name, PotSize, PlantSize, SalePrice, PurchasePrice, Picture)" +
                                                       "VALUES(@Name, @PotSize, @PlantSize, @SalePrice, @PurchasePrice, @Picture)" +
                                                       "SELECT @@IDENTITY", con))
                {
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = flower.Name;
                    cmd.Parameters.Add("@PotSize", SqlDbType.NVarChar).Value = flower.PotSize;
                    cmd.Parameters.Add("@PlantSize", SqlDbType.NVarChar).Value = flower.PlantSize;
                    cmd.Parameters.Add("@SalePrice", SqlDbType.Float).Value = flower.SalePrice;
                    cmd.Parameters.Add("@PurchasePrice", SqlDbType.Float).Value = flower.PurchasePrice;
                    cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = flower.Picture;

                    flower.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    _flowerProducts.Add(flower);
                }
            }
        }

        public List<Flower> GetAll()
        {
            List<Flower> flowers = new List<Flower>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, PotSize, PlantSize, SalePrice, PurchasePrice, Picture, IsDeleted FROM Flower WHERE IsDeleted = 0", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Flower flower = new Flower
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
                        flowers.Add(flower);
                    }
                }
            }
            return flowers;
        }

        public Flower? GetById(int id)
        {
            Flower? flower = null;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, PotSize, PlantSize, SalePrice, PurchasePrice, Picture, IsDeleted FROM Flower WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        flower = new Flower
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
            return flower;
        }

        public void Remove(Flower flower)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Flower SET IsDeleted = 1 WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", flower.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Flower flower)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Flower SET Name = @Name, PotSize = @PotSize, " +
                    "PlantSize = @PlantSize, SalePrice = @SalePrice, PurchasePrice = @PurchasePrice, Picture = @Picture WHERE Id = @Id", con);

                cmd.Parameters.Add("@Id", SqlDbType.NVarChar).Value = flower.Id;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = flower.Name;
                cmd.Parameters.Add("@PotSize", SqlDbType.NVarChar).Value = flower.PotSize;
                cmd.Parameters.Add("@PlantSize", SqlDbType.NVarChar).Value = flower.PlantSize;
                cmd.Parameters.Add("@SalePrice", SqlDbType.Float).Value = flower.SalePrice;
                cmd.Parameters.Add("@PurchasePrice", SqlDbType.Float).Value = flower.PurchasePrice;
                cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = flower.Picture;
                cmd.ExecuteNonQuery();
            }
        }
    }
}