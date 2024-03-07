using dtMauiAPp.Views;

namespace dtMauiAPp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(WeatherForecastPage), typeof(WeatherForecastPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}
