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
    public class WreathRepo : IRepo<Wreath>
    {
        private readonly string ConnectionString;
        private List<Wreath> _wreaths;

        public WreathRepo()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _wreaths = new List<Wreath>();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Wreath wreath)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Wreath (Name, PurchasePrice, SalePrice, Size, Amount, Picture)" +
                                                       "VALUES(@Name, @PurchasePrice, @SalePrice, @Size, @Amount, @Picture)" +
                                                       "SELECT @@IDENTITY", con))
                {
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = wreath.Name;
                    cmd.Parameters.Add("@PurchasePrice", SqlDbType.Float).Value = wreath.PurchasePrice;
                    cmd.Parameters.Add("@SalePrice", SqlDbType.Float).Value = wreath.SalePrice;
                    cmd.Parameters.Add("@Size", SqlDbType.NVarChar).Value = wreath.Size;
                    cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = wreath.Amount;
                    cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = wreath.Picture;

                    wreath.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    _wreaths.Add(wreath);
                }
            }
        }

        public List<Wreath> GetAll()
        {
            List<Wreath> wreaths = new List<Wreath>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, PurchasePrice, SalePrice, Size, Amount, Picture FROM Wreath", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Wreath wreath = new Wreath
                        {
                            Id = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            PurchasePrice = dr.GetDouble(2),
                            SalePrice = dr.GetDouble(3),
                            Size = (WreathSize)Enum.Parse(typeof(WreathSize), dr.GetString(4)),
                            Amount = dr.GetInt32(5),
                            Picture = (byte[])dr.GetSqlBinary(6).Value
                        };
                        wreaths.Add(wreath);
                    }
                }
            }
            return wreaths;
        }

        public Wreath? GetById(int id)
        {
            Wreath? wreath = null;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, PurchasePrice, SalePrice, Size, Amount, Picture FROM Wreath WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        wreath = new Wreath
                        {
                            Id = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            PurchasePrice = dr.GetDouble(2),
                            SalePrice = dr.GetDouble(3),
                            Size = (WreathSize)Enum.Parse(typeof(WreathSize), dr.GetString(4)),
                            Amount = dr.GetInt32(5),
                            Picture = (byte[])dr.GetSqlBinary(6).Value
                        };
                    }
                }
            }
            return wreath;
        }

        public void Remove(Wreath wreath)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Wreath WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", wreath.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Wreath wreath)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Wreath SET Name = @Name, PurchasePrice = @PurchasePrice, " +
                                                "SalePrice = @SalePrice, Size = @Size, Amount = @Amount, Picture = @Picture WHERE Id = @Id", con);

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = wreath.Id;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = wreath.Name;
                cmd.Parameters.Add("@PurchasePrice", SqlDbType.Float).Value = wreath.PurchasePrice;
                cmd.Parameters.Add("@SalePrice", SqlDbType.Float).Value = wreath.SalePrice;
                cmd.Parameters.Add("@Size", SqlDbType.NVarChar).Value = wreath.Size;
                cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = wreath.Amount;
                cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = wreath.Picture;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
