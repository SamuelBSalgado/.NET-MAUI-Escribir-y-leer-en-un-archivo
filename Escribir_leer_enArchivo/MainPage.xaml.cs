namespace Escribir_leer_enArchivo;
using Microsoft.Extensions.Configuration;
using Escribir_leer_enArchivo.Models;
using Escribir_leer_enArchivo.Database;
using Escribir_leer_enArchivo.Views;

public partial class MainPage : ContentPage

{

    private const string RememberMeKey = "RememberMe";

    private const string UsernameKey = "Username";

    private const string PasswordKey = "Password";

    private Connection dbConn = new Connection(
        Path.Combine(
            Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "mydb.db"));
    public MainPage()

    {

        InitializeComponent();

        // Cargar datos guardados si están disponibles
        if (Preferences.Get(RememberMeKey, false))
        {

            UsernameEntry.Text = Preferences.Get(UsernameKey, string.Empty);

            PasswordEntry.Text = Preferences.Get(PasswordKey, string.Empty);

            RememberMeCheckbox.IsChecked = true;

        }

    }

    async private void OnLoginButtonClicked(object sender, EventArgs e)

    {

        string username = UsernameEntry.Text;

        string password = PasswordEntry.Text;

        bool rememberMe = RememberMeCheckbox.IsChecked;

        // Aquí realizarías la lógica de autenticación.
        User currentUser = dbConn.LoginUser(username, password);
        if (currentUser != null)
        {
            /*var AgendaPage = new Agenda();
            NavigationPage navigation = new NavigationPage(AgendaPage);
            Application.Current.MainPage = navigation;*/

            var AgendaPage = new Agenda();
            AgendaPage.nombre = currentUser.nombre;
            AgendaPage.direccion = currentUser.direccion;
            AgendaPage.telefono = currentUser.telefono;
            AgendaPage.correo = currentUser.correo;
            await Shell.Current.Navigation.PushAsync(AgendaPage);
        }
        else
        {
            await DisplayAlert("Error", "Cuenta no encontrada", "OK");
        }


        if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))

        {

            if (rememberMe)

            {

                // Guardar los datos de autenticación en las preferencias

                Preferences.Set(UsernameKey, username);

                Preferences.Set(PasswordKey, password);

                Preferences.Set(RememberMeKey, true);

            }

            else

            {

                // Si no se recuerdan los datos, eliminarlos de las preferencias

                Preferences.Remove(UsernameKey);

                Preferences.Remove(PasswordKey);

                Preferences.Set(RememberMeKey, false);

            }
        }
        else
        {
            DisplayAlert("Error", "Please enter both username and password.", "OK");

        }

    }

    private void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        var registerPage = new Register();
        NavigationPage navigation = new NavigationPage(registerPage);
        Application.Current.MainPage = navigation;
    }

}
