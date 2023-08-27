namespace Escribir_leer_enArchivo
{
    public partial class MainPage : ContentPage
    {
        private string filePath;
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            // Obtener el nombre del archivo y el texto del editor
            string fileName = fileNameEntry.Text;
            string inputText = textEditor.Text;

            // Validar si el nombre del archivo no está vacío
            if (string.IsNullOrWhiteSpace(fileName))
            {
                DisplayAlert("Error", "Ingrese un nombre de archivo válido.", "Aceptar");
                return;
            }

            // Guardar el texto en el archivo especificado
            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            File.WriteAllText(filePath, inputText);

            // Mostrar mensaje de éxito
            DisplayAlert("Texto guardado", "El texto se ha guardado correctamente.", "Aceptar");
        }

        private void OnLoadButtonClicked(object sender, EventArgs e)
        {
            string fileName = fileNameEntry.Text;

            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            // Validar si el archivo existe
            if (File.Exists(filePath))
            {
                // Leer el texto desde el archivo
                string savedText = File.ReadAllText(filePath);

                // Mostrar el texto en el editor
                textEditor.Text = savedText;

                // Actualizar etiqueta para mostrar el nombre del archivo cargado
                resultLabel.Text = $"Archivo cargado: {Path.GetFileName(filePath)}";
            }
            else
            {
                // Mostrar mensaje si el archivo no existe
                DisplayAlert("Error", "El archivo especificado no existe.", "Aceptar");
            }
        }
    }
}