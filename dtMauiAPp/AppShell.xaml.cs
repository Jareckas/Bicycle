using dtMauiAPp.Views;
using Microsoft.Maui.Controls;

namespace dtMauiAPp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AuthenticationPage), typeof(AuthenticationPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(WeatherForecastPage), typeof(WeatherForecastPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }

        private async void ShellItemButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var shellItem = button.BindingContext as BaseShellItem;

            // Navigate to a new tab if it is not the current tab
            if (!CurrentState.Location.OriginalString.Contains(shellItem.Route))
                await GoToAsync($"///{shellItem.Route}");
        }
    }
}
