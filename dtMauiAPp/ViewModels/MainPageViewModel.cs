
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
                string userEmail = JsonSerializer.Deserialize<LoginResponse>(serializedLoginResponseInStorage)?.UserName;
                if (!string.IsNullOrEmpty(userEmail))
                {
                    int atIndex = userEmail.IndexOf('@');
                    if (atIndex != -1)
                    {
                        UserName = userEmail.Substring(0, atIndex);
                        return;
                    }
                }
            }
        }

        [RelayCommand]
        private async Task GoToWeatherForecast()
        {
            await Shell.Current.GoToAsync(nameof(WeatherForecastPage));
        }

        [RelayCommand]
        private async Task Home()
        {
            // Navigate to HomePage
            await Shell.Current.GoToAsync(nameof(MainPage));
        }

        [RelayCommand]
        private async Task Settings()
        {
            // Navigate to SettingsPage
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }
    }
}
