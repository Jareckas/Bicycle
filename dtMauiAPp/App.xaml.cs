using dtMauiAPp.ViewModels;
using Microsoft.Maui.Controls;

namespace dtMauiAPp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var appShell = new AppShell();
            MainPage = appShell;

            var authenticationPageViewModel = new AuthenticationPageViewModel();
            appShell.Navigation.PushAsync(new AuthenticationPage(authenticationPageViewModel));
        }
    }
}
