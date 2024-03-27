
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using dtMauiAPp.Views;
using dtMauiAPp.Models;
using dtMauiAPp.Services;
using System.Text.Json;

namespace dtMauiAPp.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private string userName;
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        private readonly ClientService clientService;

        public MainPageViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            _ = GetUserNameFromDatabase();
        }

        [RelayCommand]
        private async Task Logout()
        {
            // Remove authentication details
            SecureStorage.Default.Remove("Authentication");

            // Navigate to AuthenticationPage
            await Shell.Current.GoToAsync(nameof(AuthenticationPage));
        }

        public async Task GetUserNameFromDatabase()
        {
            UserName = await clientService.GetUsernameFromDatabase();
        }

        [RelayCommand]
        private async Task GoToWeatherForecast()
        {
            await Shell.Current.GoToAsync(nameof(WeatherForecastPage));
        }
    }
}
