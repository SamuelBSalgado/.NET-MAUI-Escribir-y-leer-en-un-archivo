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
        inNombre.TextChanged += eventoEntryVacio;

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

    private void eventoEntryVacio(object sender, TextChangedEventArgs e)
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

    private void Guardar_Clicked(object sender, EventArgs e)
    {

    }
    private void Buscar_Clicked(object sender, EventArgs e)
    {

    }
    private void Eliminar_Clicked(object sender, EventArgs e)
    {

    }
    private void Modificar_Clicked(object sender, EventArgs e)
    {

    }

}