namespace djaramillo6A.Views
{
    public partial class vAgregarEstudiante : ContentPage
    {
        public vAgregarEstudiante()
        {
            InitializeComponent();
        }

        private async void btnAgregar_Clicked(object? sender, EventArgs e)
        {
            await App.EstudianteRepo.AddNewEstudiante(txtNombre.Text, txtApellido.Text, txtEdad.Text);

            // Si hubo error se muestra y se queda en la vista; si no, se vuelve a la lista
            if (App.EstudianteRepo.StatusMessage.StartsWith("Error"))
            {
                await DisplayAlertAsync("Agregar", App.EstudianteRepo.StatusMessage, "OK");
                return;
            }

            await Navigation.PopAsync();
        }

        private async void btnCancelar_Clicked(object? sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
