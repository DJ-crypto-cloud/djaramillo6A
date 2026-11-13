using djaramillo6A.Repositories;

namespace djaramillo6A
{
    public partial class App : Application
    {
        public static EstudianteRepository EstudianteRepo { get; private set; } = null!;

        public App(EstudianteRepository repo)
        {
            InitializeComponent();
            EstudianteRepo = repo;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
