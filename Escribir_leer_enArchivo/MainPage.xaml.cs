namespace Escribir_leer_enArchivo;
using Microsoft.Extensions.Configuration;

public partial class MainPage : ContentPage

{

    private const string RememberMeKey = "RememberMe";

    private const string UsernameKey = "Username";

    private const string PasswordKey = "Password";



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



    private void OnLoginButtonClicked(object sender, EventArgs e)

    {

        string username = UsernameEntry.Text;

        string password = PasswordEntry.Text;

        bool rememberMe = RememberMeCheckbox.IsChecked;



        // Aquí realizarías la lógica de autenticación.

        // En este ejemplo, simplemente mostramos un mensaje de éxito.



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



            // Realizar la lógica de autenticación aquí, por ejemplo, navegación a la siguiente página

            DisplayAlert("Success", "Login successful!", "OK");

        }

        else

        {

            DisplayAlert("Error", "Please enter both username and password.", "OK");

        }

    }

}
    