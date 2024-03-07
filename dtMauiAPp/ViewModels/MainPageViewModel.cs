
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
        [ObservableProperty]
        private string userName;

        private readonly ClientService clientService;
        public MainPageViewModel(ClientService clientService)
        {
            this.clientService = clientService;
            _ = GetUserNameFromSecuredStorage();
        }

        [RelayCommand]
        private async Task Logout()
        {
            // Remove authentication details
            SecureStorage.Default.Remove("Authentication");

            // Navigate to AuthenticationPage
            await Shell.Current.GoToAsync("//AuthenticationPage");
        }


        public async Task GetUserNameFromSecuredStorage()
        {
            var serializedLoginResponseInStorage = await SecureStorage.Default.GetAsync("Authentication");
            if (serializedLoginResponseInStorage != null)
            {
                UserName = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)!.UserName!;
                return;
            }
        }

        [RelayCommand]
        private async Task GoToWeatherForecast()
        {
            await Shell.Current.GoToAsync(nameof(WeatherForecastPage));
        }
    }
}
