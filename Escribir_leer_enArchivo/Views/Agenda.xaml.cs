namespace Escribir_leer_enArchivo.Views;

public partial class Agenda : ContentPage
{
    public string nombre { get; set; }
    public string direccion { get; set; }
    public int telefono { get; set; }
    public string correo { get; set; }
    public Agenda()
    {
        InitializeComponent();
        System.Diagnostics.Debug.WriteLine(nombre);
    }
}