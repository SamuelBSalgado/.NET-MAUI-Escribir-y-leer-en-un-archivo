namespace Escribir_leer_enArchivo.Views;
using Escribir_leer_enArchivo.Database;
using Escribir_leer_enArchivo.Models;

public partial class Agenda : ContentPage
{
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string Correo { get; set; }

    private Connection dbConn = new Connection(
        Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "mydb.db"));
    public Agenda()
    {
        InitializeComponent();
        inNombre.TextChanged += EventoEntryVacio;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        inNombre.Text = Nombre;
        inDireccion.Text = Direccion;
        inTelefono.Text = Telefono.ToString();
        inCorreo.Text = Correo;
        GuardarButton.IsEnabled = false;
    }

    private void EventoEntryVacio(object sender, TextChangedEventArgs e)
    {
        string texto = e.NewTextValue;

        if (texto != Nombre)
        {
            GuardarButton.IsEnabled = true;

        }
        else
        {
            GuardarButton.IsEnabled = false;
        }
    }
    private void LimpiarCampos()
    {
        inNombre.Text = "";
        inDireccion.Text = "";
        inTelefono.Text = "";
        inCorreo.Text = "";
    }
    private void LlenarCampos(string nombre, string direccion,
        string telefono, string correo)
    {
        EventoEntryVacio(inNombre, new TextChangedEventArgs(inNombre.Text, nombre));
        inDireccion.Text = direccion;
        inTelefono.Text = telefono;
        inCorreo.Text = correo;
    }
    private void Guardar_Clicked(object sender, EventArgs e)
    {
        User newUser = new User(null, inNombre.Text, inDireccion.Text,
            inTelefono.Text, inCorreo.Text);
        string newId = dbConn.SaveUser(newUser);
        if (newId != null)
        {
            newUser.Id = newId;
            // Muestra el DisplayAlert después de la espera
            DisplayAlert("Correcto", "Contacto Guardado Con Éxito", "OK");
            Nombre = null;
            LimpiarCampos();
        }
        else
        {
            DisplayAlert("Error", "Error al registrar usuario", "OK");
        }
    }

    private void Buscar_Clicked(object sender, EventArgs e)
    {
        try
        {
            User searchedUser = dbConn.GetUser(inNombre.Text);
            dbConn.GetUsers();
            if (searchedUser != null)
            {
                Nombre = searchedUser.nombre;
                LlenarCampos(searchedUser.nombre, searchedUser.direccion,
                    searchedUser.telefono, searchedUser.correo);
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }

    }
    private void Eliminar_Clicked(object sender, EventArgs e)
    {
        try
        {
            User userToDelete = dbConn.GetUser(Nombre);
            if (dbConn.DeleteUser(userToDelete.Id))
            {
                DisplayAlert("Correcto", $"Contacto {inNombre} Eliminado", "OK");
                Nombre = null;
                LimpiarCampos();
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }
    }
    private void Modificar_Clicked(object sender, EventArgs e)
    {
        try
        {
            User userToEdit = dbConn.GetUser(Nombre);
            System.Diagnostics.Debug.WriteLine(userToEdit.Id);
            System.Diagnostics.Debug.WriteLine(userToEdit.nombre);
            if (userToEdit != null)
            {
                userToEdit.nombre = inNombre.Text;
                userToEdit.direccion = inDireccion.Text;
                userToEdit.telefono = inTelefono.Text;
                userToEdit.correo = inCorreo.Text;
                if (dbConn.EditUser(userToEdit))
                {
                    DisplayAlert("Correcto", $"Usuario {userToEdit.nombre} Editado", "OK");
                }
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", ex.Message, "OK");
        }
    }

}