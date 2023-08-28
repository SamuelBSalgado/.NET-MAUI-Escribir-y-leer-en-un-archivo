using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Escribir_leer_enArchivo.Models;

namespace Escribir_leer_enArchivo.Database
{
    public class Connection
    {
        public readonly string connectionString;

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
                    "CREATE TABLE IF NOT EXISTS Users " +
                    "(Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "nombre TEXT, direccion TEXT, " +
                    "telefono INTEGER, " +
                    "correo TEXT, pass TEXT)";
                try
                {
                    createTableCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }

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
                        Users.Add(new User(reader.GetString(2), reader.GetString(3),
                            reader.GetString(4), reader.GetString(5)));
                    }
                }
                return Users;
            }
        }
        public User GetUser(string inNombre)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var selectCommand = connection.CreateCommand();
                selectCommand.CommandText = "SELECT " +
                    "nombre, direccion," +
                    "telefono, correo FROM Users " +
                    "WHERE nombre=@nombre";

                selectCommand.Parameters.AddWithValue("@nombre", inNombre);
                using (var reader = selectCommand.ExecuteReader())
                {
                    try
                    {
                        reader.Read();
                        if (reader.HasRows)
                        {
                            string nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                            string direccion = reader.GetString(reader.GetOrdinal("direccion"));
                            string telefono = reader.GetString(reader.GetOrdinal("telefono"));
                            string correo = reader.GetString(reader.GetOrdinal("correo"));

                            User searchedUser = new User(nombre, direccion, telefono, correo);
                            return searchedUser;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                        return null;
                    }

                }
            }
        }
        public User LoginUser(string nombreIN, string passwordIN)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var selectCommand = connection.CreateCommand();
                selectCommand.CommandText = "SELECT nombre, direccion, " +
                    "telefono, correo " +
                    "FROM Users WHERE nombre=@nombre AND pass=@password";
                selectCommand.Parameters.AddWithValue("@nombre", nombreIN);
                selectCommand.Parameters.AddWithValue("@password", passwordIN);

                try
                {
                    using (var reader = selectCommand.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.HasRows)
                        {
                            string nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                            string direccion = reader.GetString(reader.GetOrdinal("direccion"));
                            string telefono = reader.GetString(reader.GetOrdinal("telefono"));
                            string correo = reader.GetString(reader.GetOrdinal("correo"));

                            User searchedUser = new User(nombre, direccion, telefono, correo);
                            return searchedUser;
                        }
                        else
                        {
                            return null;
                        }

                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                    return null;
                }
            }
        }

        public bool SaveUser(User User)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = "INSERT INTO Users (nombre, direccion, " +
                    "telefono, correo, pass) " +
                    "VALUES (@nombre, @direccion, " +
                    "@telefono, @correo, @pass)";
                insertCommand.Parameters.AddWithValue("@nombre", User.nombre);
                insertCommand.Parameters.AddWithValue("@direccion", User.direccion);
                insertCommand.Parameters.AddWithValue("@telefono", User.telefono);
                insertCommand.Parameters.AddWithValue("@correo", User.correo);
                insertCommand.Parameters.AddWithValue("@pass", User.password);
                try
                {
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                    return false;
                }


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
