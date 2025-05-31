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
    public class NoteRepo : IRepo<Note>
    {
        private readonly string ConnectionString;
        private List<Note> _notes;

        public NoteRepo()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _notes = new List<Note>();
            ConnectionString = config.GetConnectionString("MyDBConnection");
        }

        public void Add(Note note)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Note (Title, Description, Picture)" +
                                                       "VALUES(@Title, @Description, @Picture)" +
                                                       "SELECT @@IDENTITY", con))
                {
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = note.Title;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = note.Description;
                    cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = note.Picture;

                    note.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    _notes.Add(note);
                }
            }
        }

        public List<Note> GetAll()
        {
            List<Note> notes = new List<Note>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Title, Description, Picture FROM Note", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Note note = new Note
                        {
                            Id = dr.GetInt32(0),
                            Title = dr.GetString(1),
                            Description = dr.GetString(2),
                            Picture = (byte[])dr.GetSqlBinary(3).Value
                        };
                        notes.Add(note);
                    }
                }
            }
            return notes;
        }

        public Note? GetById(int id)
        {
            Note? note = null;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Title, Description, Picture FROM Note WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        note = new Note
                        {
                            Id = dr.GetInt32(0),
                            Title = dr.GetString(1),
                            Description = dr.GetString(2),
                            Picture = (byte[])dr.GetSqlBinary(3).Value
                        };
                    }
                }
            }
            return note;
        }

        public void Remove(Note note)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Note WHERE Id = @Id", con);
                cmd.Parameters.AddWithValue("@Id", note.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Note note)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Note SET Title = @Title, Description = @Description, " +
                    "Picture = @Picture WHERE Id = @Id", con);

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = note.Id;
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = note.Title;
                cmd.Parameters.Add("@Description", SqlDbType.NVarChar).Value = note.Description;
                cmd.Parameters.Add("@Picture", SqlDbType.VarBinary).Value = note.Picture;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
