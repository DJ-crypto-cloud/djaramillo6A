namespace djaramillo6A.Utils
{
    public static class ApiConfig
    {
        // El emulador de Android ve la PC como 10.0.2.2.
        // En Windows se usa localhost. En un celular fisico: IP de la PC en la red WiFi.
        private static string Host =>
            DeviceInfo.Platform == DevicePlatform.Android ? "10.0.2.2" : "localhost";

        public static string BaseUrl => $"http://{Host}/ws_estudiantes/estudiante_ws.php";
    }
}
