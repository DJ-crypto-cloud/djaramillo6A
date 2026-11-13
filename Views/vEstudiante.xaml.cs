using djaramillo6A.Models;

namespace djaramillo6A.Views
{
    public partial class vEstudiante : ContentPage
    {
        public vEstudiante()
        {
            InitializeComponent();
        }

        // Se ejecuta cada vez que la vista aparece (al abrir y al volver de otra vista)
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listaEstudiantes.ItemsSource = await App.EstudianteRepo.GetAllEstudiantes();
            lblStatusMessage.Text = App.EstudianteRepo.StatusMessage;
        }

        private async void btnAgregar_Clicked(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new vAgregarEstudiante());
        }

        private async void listaEstudiantes_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (listaEstudiantes.SelectedItem is not Estudiante estudiante)
                return;

            // Se quita la seleccion para poder elegir el mismo estudiante otra vez
            listaEstudiantes.SelectedItem = null;
            await Navigation.PushAsync(new vActualizarEliminar(estudiante));
        }
    }
}
