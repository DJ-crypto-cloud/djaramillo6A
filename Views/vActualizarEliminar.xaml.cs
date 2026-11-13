using djaramillo6A.Models;

namespace djaramillo6A.Views
{
    public partial class vActualizarEliminar : ContentPage
    {
        // El estudiante llega desde la lista por el constructor
        public vActualizarEliminar(Estudiante estudiante)
        {
            InitializeComponent();

            txtCodigo.Text = estudiante.Codigo.ToString();
            txtNombre.Text = estudiante.Nombre;
            txtApellido.Text = estudiante.Apellido;
            txtEdad.Text = estudiante.Edad.ToString();
        }

        private async void btnActualizar_Clicked(object? sender, EventArgs e)
        {
            int codigo = int.Parse(txtCodigo.Text);
            await App.EstudianteRepo.UpdateEstudiante(codigo, txtNombre.Text, txtApellido.Text, txtEdad.Text);

            if (App.EstudianteRepo.StatusMessage.StartsWith("Error"))
            {
                await DisplayAlertAsync("Actualizar", App.EstudianteRepo.StatusMessage, "OK");
                return;
            }

            await Navigation.PopAsync();
        }

        private async void btnEliminar_Clicked(object? sender, EventArgs e)
        {
            bool confirmar = await DisplayAlertAsync("Eliminar", $"¿Eliminar a {txtNombre.Text}?", "Sí", "No");
            if (!confirmar)
                return;

            int codigo = int.Parse(txtCodigo.Text);
            await App.EstudianteRepo.DeleteEstudiante(codigo);

            if (App.EstudianteRepo.StatusMessage.StartsWith("Error"))
            {
                await DisplayAlertAsync("Eliminar", App.EstudianteRepo.StatusMessage, "OK");
                return;
            }

            await Navigation.PopAsync();
        }
    }
}
