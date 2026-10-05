using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using UzenetKuldo.Models;

namespace UzenetKuldo.Services
{
    public class UzenetServices
    {
        private string kapcsolat = ConfigurationManager.ConnectionStrings["UzenetDb"].ConnectionString;

        public List<Uzenet> Read()
        {
            List<Uzenet> uzenetek = new List<Uzenet>();
            using (MySqlConnection conn = new MySqlConnection(kapcsolat))
            {
                conn.Open();
                string sql = "SELECT * FROM uzenet ORDER BY Id";
                using (MySqlCommand command = new MySqlCommand(sql, conn))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Uzenet uzenet = new Uzenet();
                        uzenet.Id = Convert.ToInt32(reader["Id"]);
                        uzenet.Szoveg = reader["Szoveg"].ToString();
                        uzenet.KuldesiIdo = Convert.ToDateTime(reader["KüldesiIdo"]);
                        uzenet.UzenetTipus = reader["UzenetTipus"].ToString();
                        if (reader["Telefon"] == DBNull.Value)
                        {
                            uzenet.Telefon = null;
                        }
                        else
                        {
                            uzenet.Telefon = reader["Telefon"].ToString();
                        }
                        if (reader["Email"] == DBNull.Value)
                        {
                            uzenet.Email = null;
                        }
                        else
                        {
                            uzenet.Email = reader["Email"].ToString();
                        }
                        uzenetek.Add(uzenet);
                    }
                }
            }
            return uzenetek;
        }

        public bool Exists(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(kapcsolat))
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM uzenet WHERE Id = @Id";
                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }

        public int Create(Uzenet uzenet)
        {
            using (MySqlConnection conn = new MySqlConnection(kapcsolat))
            {
                conn.Open();
                string sql = "INSERT INTO uzenet (Szoveg, `KüldesiIdo`, UzenetTipus, Telefon, Email) " +
                    "VALUES (@Szoveg, @KuldesiIdo, @UzenetTipus, @Telefon, @Email)";
                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    Parameterek(command, uzenet);
                    command.ExecuteNonQuery();
                    return Convert.ToInt32(command.LastInsertedId);
                }
            }
        }

        public bool Update(int id, Uzenet uzenet)
        {
            using (MySqlConnection conn = new MySqlConnection(kapcsolat))
            {
                conn.Open();
                string sql = "UPDATE uzenet SET Szoveg = @Szoveg, `KüldesiIdo` = @KuldesiIdo, " +
                    "UzenetTipus = @UzenetTipus, Telefon = @Telefon, Email = @Email WHERE Id = @Id";
                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    Parameterek(command, uzenet);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Delete(int id)
        {
            using (MySqlConnection conn = new MySqlConnection(kapcsolat))
            {
                conn.Open();
                string sql = "DELETE FROM uzenet WHERE Id = @Id";
                using (MySqlCommand command = new MySqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        private void Parameterek(MySqlCommand command, Uzenet uzenet)
        {
            command.Parameters.AddWithValue("@Szoveg", uzenet.Szoveg);
            command.Parameters.AddWithValue("@KuldesiIdo", uzenet.KuldesiIdo);
            command.Parameters.AddWithValue("@UzenetTipus", uzenet.UzenetTipus);
            if (uzenet.Telefon == null)
            {
                command.Parameters.AddWithValue("@Telefon", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Telefon", uzenet.Telefon);
            }
            if (uzenet.Email == null)
            {
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", uzenet.Email);
            }
        }
    }
}
