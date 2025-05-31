using FlowerShop.Models;
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

        public void Add(Miscellaneous miscellaneous)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Miscellaneous (Name, PurchasePrice, Amount, Picture)" +
                                                       "VALUES(@Name, @PurchasePrice, @Amount, @Picture)" +
                                                       "SELECT @@IDENTITY", con))
                {
                    cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = miscellaneous.Name;
                    cmd.Parameters.Add("@PurchasePrice", SqlDbType.Float).Value = miscellaneous.PurchasePrice;
                    cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = miscellaneous.Amount;
                    cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = miscellaneous.Picture;

                    miscellaneous.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    _miscellaneousProducts.Add(miscellaneous);
                }
            }
        }

        public List<Miscellaneous> GetAll()
        {
            List<Miscellaneous> miscellaneouss = new List<Miscellaneous>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, PurchasePrice, Amount, Picture FROM Miscellaneous", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Miscellaneous miscellaneous = new Miscellaneous
                        {
                            Id = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            PurchasePrice = dr.GetDouble(2),
                            Amount = dr.GetInt32(3),
                            Picture = (byte[])dr.GetSqlBinary(4).Value
                        };
                        miscellaneouss.Add(miscellaneous);
                    }
                }
            }
            return miscellaneouss;
        }

        public Miscellaneous? GetById(int id)
        {
            Miscellaneous? miscellaneouss = null;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Name, PurchasePrice, Amount, Picture FROM Miscellaneous WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        miscellaneouss = new Miscellaneous
                        {
                            Id = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            PurchasePrice = dr.GetDouble(2),
                            Amount = dr.GetInt32(3),
                            Picture = (byte[])dr.GetSqlBinary(4).Value
                        };
                    }
                }
            }
            return miscellaneouss;
        }

        public void Remove(Miscellaneous miscellaneous)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Miscellaneous WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", miscellaneous.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Miscellaneous miscellaneous)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Miscellaneous SET Name = @Name, PurchasePrice = @PurchasePrice, " +
                    "Amount = @Amount, Picture = @Picture WHERE Id = @Id", con);

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = miscellaneous.Id;
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = miscellaneous.Name;
                cmd.Parameters.Add("@PurchasePrice", SqlDbType.Float).Value = miscellaneous.PurchasePrice;
                cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = miscellaneous.Amount;
                cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = miscellaneous.Picture;
                cmd.ExecuteNonQuery();
            }
        }
    }
}