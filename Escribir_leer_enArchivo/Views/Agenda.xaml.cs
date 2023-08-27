namespace Escribir_leer_enArchivo.Views;

public partial class Agenda : ContentPage
{
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public string Correo { get; set; }
    public Agenda()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        lblNombre.Text = Nombre;
        lblDireccion.Text = Direccion;
        lblTelefono.Text = Telefono.ToString();
        lblCorreo.Text = Correo;
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