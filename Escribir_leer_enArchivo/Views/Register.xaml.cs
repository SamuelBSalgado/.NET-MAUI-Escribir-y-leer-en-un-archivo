namespace Escribir_leer_enArchivo.Views;
using Escribir_leer_enArchivo.Database;
using Escribir_leer_enArchivo.Models;
using System.Data.Common;

public partial class Register : ContentPage
{
    private Connection dbConn = new Connection(
        Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "mydb.db"));
    public Register()
    {
        InitializeComponent();
    }

    async private void Button_Clicked(object sender, EventArgs e)
    {
        User newUser = new User(username.Text, direccion.Text,
            telefono.Text, correo.Text, password.Text);
        if (dbConn.SaveUser(newUser))
        {
            await Task.Delay(3000);

            // Muestra el DisplayAlert después de la espera
            await DisplayAlert("Success", "Cuenta creada con éxito", "OK");
            var loginPage = new MainPage();
            NavigationPage navigation = new NavigationPage(loginPage);
            Application.Current.MainPage = navigation;
        }
        else
        {
            await DisplayAlert("Error", "Error al registrar usuario", "OK");
        }
    }
}