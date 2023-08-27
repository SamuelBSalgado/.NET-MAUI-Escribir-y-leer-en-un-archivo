using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Escribir_leer_enArchivo.Models;

namespace Escribir_leer_enArchivo.Database
{
    internal class Connection
    {
        private readonly string connectionString;

        public Connection(string dbPath)
        {
            connectionString = $"Data Source={dbPath}";
            InitializeDatabase();           
        }

        private void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var createTableCommand = connection.CreateCommand();
                createTableCommand.CommandText =
                    "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY, Nombre TEXT, Descripcion TEXT)";
                createTableCommand.ExecuteNonQuery();
            }
        }

        public List<User> GetUsers()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var selectCommand = connection.CreateCommand();
                selectCommand.CommandText = "SELECT * FROM Users";

                var Users = new List<User>();
                using (var reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Users.Add(new User
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1),
                            Descripcion = reader.GetString(2)
                        });
                    }
                }

                return Users;
            }
        }

        public void SaveUser(User User)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = "INSERT INTO Users (Nombre, Descripcion) VALUES (@nombre, @descripcion)";
                insertCommand.Parameters.AddWithValue("@nombre", User.Nombre);
                insertCommand.Parameters.AddWithValue("@descripcion", User.Descripcion);
                insertCommand.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var deleteCommand = connection.CreateCommand();
                deleteCommand.CommandText = "DELETE FROM Users WHERE Id = @id";
                deleteCommand.Parameters.AddWithValue("@id", id);
                deleteCommand.ExecuteNonQuery();
            }
        }
    }
}
