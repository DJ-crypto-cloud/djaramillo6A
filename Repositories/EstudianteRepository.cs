using djaramillo6A.Models;
using djaramillo6A.Utils;
using Newtonsoft.Json;

namespace djaramillo6A.Repositories
{
    public class EstudianteRepository
    {
        private readonly HttpClient _client = new() { Timeout = TimeSpan.FromSeconds(10) };

        public string StatusMessage { get; set; } = string.Empty;

        //GET
        public async Task<List<Estudiante>> GetAllEstudiantes()
        {
            StatusMessage = string.Empty;
            try
            {
                string json = await _client.GetStringAsync(ApiConfig.BaseUrl);
                return JsonConvert.DeserializeObject<List<Estudiante>>(json) ?? new List<Estudiante>();
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }

            return new List<Estudiante>();
        }

        //POST (PHP lee los datos desde $_POST, por eso se envian como formulario)
        public async Task AddNewEstudiante(string nombre, string apellido, string edad)
        {
            StatusMessage = string.Empty;
            try
            {
                Validar(nombre, apellido, edad);

                var datos = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "nombre", nombre.Trim() },
                    { "apellido", apellido.Trim() },
                    { "edad", edad.Trim() }
                });

                var respuesta = await _client.PostAsync(ApiConfig.BaseUrl, datos);
                respuesta.EnsureSuccessStatusCode();
                StatusMessage = "Dato ingresado";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        //PUT (PHP lee los datos desde $_GET, por eso van en la URL)
        public async Task UpdateEstudiante(int codigo, string nombre, string apellido, string edad)
        {
            StatusMessage = string.Empty;
            try
            {
                Validar(nombre, apellido, edad);

                string url = $"{ApiConfig.BaseUrl}?codigo={codigo}" +
                             $"&nombre={Uri.EscapeDataString(nombre.Trim())}" +
                             $"&apellido={Uri.EscapeDataString(apellido.Trim())}" +
                             $"&edad={Uri.EscapeDataString(edad.Trim())}";

                var respuesta = await _client.PutAsync(url, null);
                respuesta.EnsureSuccessStatusCode();
                StatusMessage = "Dato actualizado";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        //DELETE
        public async Task DeleteEstudiante(int codigo)
        {
            StatusMessage = string.Empty;
            try
            {
                var respuesta = await _client.DeleteAsync($"{ApiConfig.BaseUrl}?codigo={codigo}");
                respuesta.EnsureSuccessStatusCode();
                StatusMessage = "Dato eliminado";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        private static void Validar(string nombre, string apellido, string edad)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new Exception("El nombre es requerido");
            if (string.IsNullOrWhiteSpace(apellido))
                throw new Exception("El apellido es requerido");
            if (!int.TryParse(edad, out int valor) || valor <= 0)
                throw new Exception("La edad debe ser un numero mayor a 0");
        }
    }
}
