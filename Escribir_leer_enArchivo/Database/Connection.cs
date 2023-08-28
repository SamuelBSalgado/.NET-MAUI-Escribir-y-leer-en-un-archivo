using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Escribir_leer_enArchivo.Models;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            /* using (var connection = new SqliteConnection(connectionString))
             {
                 connection.Open();

                 var deleteCommand = connection.CreateCommand();
                 deleteCommand.CommandText = "DELETE FROM Users"; // Cambia "Users" al nombre de tu tabla

                 int rowsAffected = deleteCommand.ExecuteNonQuery();

                 // Verifica si se eliminaron registros
                 if (rowsAffected > 0)
                 {
                     System.Diagnostics.Debug.WriteLine($"Se eliminaron {rowsAffected} registros.");
                 }
                 else
                 {
                     System.Diagnostics.Debug.WriteLine("No se eliminaron registros.");
                 }
             }*/

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

        public void GetUsers()
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

                    }
                }
            }
        }
        public User GetUser(string inNombre)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var selectCommand = connection.CreateCommand();
                selectCommand.CommandText = "SELECT * " +
                    "FROM Users " +
                    "WHERE nombre=@nombre";

                selectCommand.Parameters.AddWithValue("@nombre", inNombre);
                using (var reader = selectCommand.ExecuteReader())
                {
                    try
                    {
                        reader.Read();
                        if (reader.HasRows)
                        {
                            string id = reader.GetString(reader.GetOrdinal("Id"));
                            string nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                            string direccion = reader.GetString(reader.GetOrdinal("direccion"));
                            string telefono = reader.GetString(reader.GetOrdinal("telefono"));
                            string correo = reader.GetString(reader.GetOrdinal("correo"));

                            User searchedUser = new User(id, nombre, direccion, telefono, correo);
                            return searchedUser;
                        }
                        else
                        {
                            throw new Exception($"El Contacto {inNombre} No Existe");
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                        throw new Exception(ex.Message);
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
                selectCommand.CommandText = "SELECT * " +
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
                            string id = reader.GetString(reader.GetOrdinal("Id"));
                            string nombre = reader.GetString(reader.GetOrdinal("Nombre"));
                            string direccion = reader.GetString(reader.GetOrdinal("direccion"));
                            string telefono = reader.GetString(reader.GetOrdinal("telefono"));
                            string correo = reader.GetString(reader.GetOrdinal("correo"));

                            User searchedUser = new User(id, nombre, direccion, telefono, correo);
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

        public string SaveUser(User user)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                System.Diagnostics.Debug.WriteLine(user.password);
                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = "INSERT INTO Users (nombre, direccion, " +
                    "telefono, correo, pass) " +
                    "VALUES (@nombre, @direccion, " +
                    "@telefono, @correo, @pass)";
                insertCommand.Parameters.AddWithValue("@nombre", user.nombre);
                insertCommand.Parameters.AddWithValue("@direccion", user.direccion);
                insertCommand.Parameters.AddWithValue("@telefono", user.telefono);
                insertCommand.Parameters.AddWithValue("@correo", user.correo);
                insertCommand.Parameters.AddWithValue("@pass", user.password);
                try
                {
                    int rowsAffected = insertCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Obtener el último Id insertado
                        var getIdCommand = connection.CreateCommand();
                        getIdCommand.CommandText = "SELECT last_insert_rowid()";
                        long nuevoId = Convert.ToInt64(getIdCommand.ExecuteScalar());
                        return nuevoId.ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                    return null;
                }
            }
        }

        public bool DeleteUser(string id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var deleteCommand = connection.CreateCommand();
                deleteCommand.CommandText = "DELETE FROM Users WHERE Id = @id";
                deleteCommand.Parameters.AddWithValue("@id", int.Parse(id));
                try
                {
                    if (deleteCommand.ExecuteNonQuery() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("No Existe el Contacto");
                    }

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                    throw new Exception(e.Message);
                }

            }
        }
        public bool EditUser(User user)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var insertCommand = connection.CreateCommand();
                insertCommand.CommandText = "UPDATE Users SET nombre = @nombre, " +
                    "direccion = @direccion, " +
                    "telefono = @telefono, " +
                    "correo = @correo " +
                    "WHERE Id = @id";
                insertCommand.Parameters.AddWithValue("@nombre", user.nombre);
                insertCommand.Parameters.AddWithValue("@direccion", user.direccion);
                insertCommand.Parameters.AddWithValue("@telefono", user.telefono);
                insertCommand.Parameters.AddWithValue("@correo", user.correo);
                insertCommand.Parameters.AddWithValue("@id", int.Parse(user.Id));
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
    }

}
