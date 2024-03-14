using CommunityToolkit.Mvvm.ComponentModel;
using dtMauiAPp.Services;
using CommunityToolkit.Mvvm.Input;
using dtMauiAPp.Views;

namespace dtMauiAPp.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly ClientService clientService;
        public SettingsViewModel(ClientService clientService)
        {
            this.clientService = clientService;
        }

        // Property to track if dark mode is enabled
        private bool isDarkModeEnabled;
        public bool IsDarkModeEnabled
        {
            get { return isDarkModeEnabled; }
            set { SetProperty(ref isDarkModeEnabled, value); }
        }

        [RelayCommand]
        private async Task Home()
        {
            // Navigate to HomePage
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task Settings()
        {
            // Navigate to SettingsPage
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }
    }
}