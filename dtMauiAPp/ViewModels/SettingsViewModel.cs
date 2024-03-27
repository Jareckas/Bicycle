using CommunityToolkit.Mvvm.ComponentModel;
using dtMauiAPp.Services;
using CommunityToolkit.Mvvm.Input;
using dtMauiAPp.Views;
using Microsoft.Extensions.Logging;

namespace dtMauiAPp.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        private readonly MainPageViewModel mainPageViewModel;
        private readonly ClientService clientService;
        private readonly ILogger<SettingsViewModel> logger;

        public SettingsViewModel(ClientService clientService, ILogger<SettingsViewModel> logger, MainPageViewModel mainPageViewModel)
        {
            this.clientService = clientService;
            this.logger = logger;
            this.mainPageViewModel = mainPageViewModel;
        }

        // Property to track if dark mode is enabled
        private bool isDarkModeEnabled;
        public bool IsDarkModeEnabled
        {
            get { return isDarkModeEnabled; }
            set { SetProperty(ref isDarkModeEnabled, value); }
        }

        [RelayCommand]
        public async Task ChangeUsernameAsync(string newUsername)
        {
            try
            {
                // Call a method in your ClientService to update the username in the database
                await clientService.UpdateUsernameAsync(newUsername);

                await mainPageViewModel.GetUserNameFromDatabase();

                logger.LogInformation("Username changed successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to change username.");
                // Handle the exception appropriately
            }
        }
    }
}